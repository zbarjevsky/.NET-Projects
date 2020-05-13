using System;
using PlaybackSoundSwitch.Device;

namespace PlaybackSoundSwitch.Device
{
    public class DeviceInfo : IEquatable<DeviceInfo>, IComparable<DeviceInfo>
    {
        public string Name { get; }
        public string FriendlyName { get; }
        public string Id { get; }
        public EDataFlow DeviceType { get; }
        public MMDevice Device { get; }

        //[JsonConstructor]
        public DeviceInfo(string name, string id, EDataFlow type)
        {
            Name = name;
            Id = id;
            DeviceType = type;
        }

        public DeviceInfo(MMDevice device)
        {
            try
            {
                Device = device;
                Id = device.ID;
                DeviceType = device.DataFlow;
                Name = device.DeviceFriendlyName;
                FriendlyName = device.FriendlyName;
            }
            catch (Exception err)
            {
                Name = "Error" + err.Message;
                FriendlyName = err.ToString();
            }
        }

        public bool Equals(DeviceInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && DeviceType == other.DeviceType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DeviceInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^ (int) DeviceType;
            }
        }

        public static bool operator ==(DeviceInfo left, DeviceInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DeviceInfo left, DeviceInfo right)
        {
            return !Equals(left, right);
        }


        public int CompareTo(DeviceInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = string.Compare(Id, other.Id, StringComparison.Ordinal);
            if (idComparison != 0) return idComparison;
            return DeviceType.CompareTo(other.DeviceType);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}