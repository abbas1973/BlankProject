namespace FajrLog
{
    public class FajrLog
    {
        public DateTime timeStamp { get; set; }
        public DateTime timeOccurrence { get; set; }
        public DateTime timeRegister { get; set; }
        public DateTime timeDuration { get; set; }
        public DateTime timeSend { get; set; }
        public DateTime timeFrom { get; set; }
        public DateTime timeTo { get; set; }


        public string actionType { get; set; }
        public string actionSubType { get; set; }
        public string actionDescription { get; set; }
        public long actionId { get; set; }
        public string actionFlag { get; set; }
        public string actionSensitivity { get; set; }
        public long logId { get; set; }
        public long logNum { get; set; }


        public string userName { get; set; }
        public string consoleUser { get; set; }
        public string nationalId { get; set; }
        public long uniqueId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public long employeeNum { get; set; }
        public string membership { get; set; }
        public string accessLevel { get; set; }
        public string nationality { get; set; }
        public string gender { get; set; }
        public string phoneNum { get; set; }
        public string groupName { get; set; }
        public string carNum { get; set; }
        public string pic { get; set; }
        public string comment { get; set; }


        public string URL { get; set; }
        public string formName { get; set; }
        public string forceName { get; set; }
        public long forceUniqueId { get; set; }
        public string orgName { get; set; }
        public long orgUniqueId { get; set; }
        public string depName { get; set; }
        public long depUniqueId { get; set; }
        public string secName { get; set; }
        public long secUniqueId { get; set; }
        public string partName { get; set; }
        public long partUniqueId { get; set; }
        public string zoneName { get; set; }
        public long zoneId { get; set; }
        public string cityName { get; set; }
        public long cityId { get; set; }
        public string gateName { get; set; }
        public string subSystemName { get; set; }


        public string appName { get; set; }
        public string appVersion { get; set; }
        public long appId { get; set; }
        public string appVendor { get; set; }
        public string appServerIP { get; set; }
        public string appServerHostName { get; set; }
        public string appPortNum { get; set; }
        public string appDBIP { get; set; }
        public string appDBName { get; set; }


        public string clientName { get; set; }
        public long clientUniqueId { get; set; }
        public string MACAddress { get; set; }
        public string HDDSerial { get; set; }
        public string IP { get; set; }
        public string OS { get; set; }
        public string userAgent { get; set; }


        public string deviceName { get; set; }
        public string deviceType { get; set; }
        public long deviceUniqueId { get; set; }
        public string deviceComment { get; set; }
        public string deviceStatus { get; set; }
        public string deviceBusType { get; set; }


        public string targetType { get; set; }
        public string targetSubType { get; set; }
        public long targetUniqueId { get; set; }
        public string targetSpec { get; set; }
        public string targetContent { get; set; }
        public string targetComment { get; set; }
        public string targetName { get; set; }
        public string targetIP { get; set; }
        public string targetUserAgent { get; set; }
        public string targetUserName { get; set; }
        public string targetUserIP { get; set; }
        //public long targetUniqueId { get; set; }
        public string targetFirstName { get; set; }
        public string targetLastName { get; set; }
        public string targetNatioanalId { get; set; }
        public long targetEmployeeNum { get; set; }
        public string targetMembership { get; set; }
        public string targetAccessLevel { get; set; }
        public string targetNationality { get; set; }
        public string targetGender { get; set; }
        public string targetSensitivity { get; set; }
        public string targetPhoneNum { get; set; }
        public string targetCarNum { get; set; }
        public string targetPic { get; set; }
        public string targetMACAddress { get; set; }
        public string targetHDDSerial { get; set; }
        public string targetConsolName { get; set; }
        public string targetSubject { get; set; }
        public string targetGroupName { get; set; }
        public long targetId { get; set; }
        public string targetHash { get; set; }
        public string targetPath { get; set; }
        public string targetSize { get; set; }
        public string targetBusType { get; set; }
        public string targetCreateTime { get; set; }
        public string targetModificationTime { get; set; }
        public string targetVendor { get; set; }
        public string targetVersion { get; set; }
        public string targetMessure { get; set; }
        public string targetAmount { get; set; }
    }
}