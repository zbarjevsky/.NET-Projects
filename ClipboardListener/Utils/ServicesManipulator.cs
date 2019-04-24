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

    public static class ServicesManipulator
    {
        private static Task _task;

        private static volatile bool StopMonitoringServices = false;
        public static volatile bool ContinuousMonitoringServices = false;

        public static Dictionary<string, ServiceController> ServicesList = 
            new Dictionary<string, ServiceController>() { { "SMS Agent Host", null } };

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
                if (ContinuousMonitoringServices)
                {
                    try
                    {
                        ServiceController[] services = ServiceController.GetServices();
                        foreach (ServiceController service in services)
                        {
                            if (ServicesList.ContainsKey(service.DisplayName))
                            {
                                if (ServicesList[service.DisplayName] != null)
                                {
                                    if (service.Status != ServicesList[service.DisplayName].Status)
                                    {
                                        Utils.Log.WriteLineF("[ServiceManipulator][MonitorServicesStatus] Service Status Change: '{0}', Status: '{1}', Start Type: '{2}'",
                                            service.DisplayName, service.Status, service.StartType);
                                    }
                                    if (service.StartType != ServicesList[service.DisplayName].StartType)
                                    {
                                        Utils.Log.WriteLineF("[ServiceManipulator][MonitorServicesStatus] Service Type Changed: '{0}', Status: '{1}', Start Type: '{2}'",
                                            service.DisplayName, service.Status, service.StartType);
                                    }
                                    //release previous instance
                                    ServicesList[service.DisplayName].Dispose();
                                    ServicesList[service.DisplayName] = null;
                                }
                                else
                                {
                                    Utils.Log.WriteLineF("[ServiceManipulator][MonitorServicesStatus] Service Info Initialized: '{0}', Status: '{1}', Start Type: '{2}'",
                                        service.DisplayName, service.Status, service.StartType);
                                }
                                ServicesList[service.DisplayName] = service;

                                if (service.Status == ServiceControllerStatus.Running)
                                {
                                    Utils.Log.WriteLineF("[ServiceManipulator][MonitorServicesStatus] Service Is Running and will be stopped: '{0}', Status: '{1}', Start Type: '{2}'",
                                       service.DisplayName, service.Status, service.StartType);
                                    service.Stop();
                                }
                            }
                            else //service is not monitored
                            {
                                service.Dispose();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        Utils.Log.WriteLine("[ServiceManipulator][MonitorServicesStatus] Exception: {0}", err.ToString());
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
