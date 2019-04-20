using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClipboardManager.Utils
{
    public class ServiceInfo
    {
        public string DisplayName;
        public ServiceControllerStatus Status;

        public ServiceStartMode StartType;
        public string ServiceName;

        public ServiceInfo(string displayName)
        {
            DisplayName = displayName;
        }

        public void Update(ServiceController svc)
        {
            DisplayName = svc.DisplayName;
            Status = svc.Status;
            StartType = svc.StartType;
            ServiceName = svc.ServiceName;
        }
    }

    public static class ServiceManipulator
    {
        private static Task _task;

        public static volatile bool StopMonitoringServices = false;
        public static volatile bool PauseMonitoringServices = false;

        public static Dictionary<string, ServiceController> ServicesList = 
            new Dictionary<string, ServiceController>() { { "SMS Agent Host", null } };

        static ServiceManipulator()
        {
            //Start();
        }

        public static bool Start()
        {
            StopMonitoringServices = false;
            if (_task == null || _task.Status != TaskStatus.Running)
            {
                _task = new Task(MonitorServicesStatus, TaskCreationOptions.LongRunning);
                _task.Start();
            }
            Utils.Log.WriteLine("[ServiceManipulator][Start] TaskStatus: '{0}'", _task.Status);
            return _task.Status == TaskStatus.Running;
        }

        public static bool Stop()
        {
            StopMonitoringServices = true;
            if (_task == null || _task.Status != TaskStatus.Running)
            {
                _task = null;
                return true;
            }

            bool stopped = _task.Wait(1500);
            stopped = _task.Status != TaskStatus.Running;
            Utils.Log.WriteLine("[ServiceManipulator][Stop] TaskStatus: '{0}'", _task.Status);
            _task = null;
            return stopped;
        }

        private static void MonitorServicesStatus()
        {
            while(!StopMonitoringServices)
            {
                if (!PauseMonitoringServices)
                {
                    try
                    {
                        ServiceController[] services = ServiceController.GetServices();
                        foreach (ServiceController service in services)
                        {
                            if (ServicesList.ContainsKey(service.DisplayName))
                            {
                                ServiceControllerStatus oldStatus = 0;
                                if (ServicesList[service.DisplayName] != null)
                                    oldStatus = ServicesList[service.DisplayName].Status;

                                ServicesList[service.DisplayName] = service;
                                if (service.Status != oldStatus)
                                {
                                    Utils.Log.WriteLine("[MonitorServicesStatus] Service: '{0}', Status: '{1}', Start Type: '{2}'", 
                                        service.DisplayName, service.Status, service.StartType);
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Utils.Log.WriteLine("Exception in MonitorServicesStatus: {0}", err.ToString());
                    }
                }

                Thread.Sleep(1000);
            }
        }

        public static void GetServiceStatuses()
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController service in services)
            {
                Debug.WriteLine(service.ServiceName + "==" + service.Status);
            }
        }
    }
}
