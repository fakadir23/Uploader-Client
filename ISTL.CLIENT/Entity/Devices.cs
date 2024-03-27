using ISTL.MODELS.DTO.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISTL.RAB.Entity
{
    public class Devices
    {
        public static List<DeviceDto> GetPhotoDeviceList()
        {

            List<DeviceDto> list = new List<DeviceDto>();

            list.Add(new DeviceDto()
            {
                Name = "Canon EOS 700D",
                DisplayName = "Canon EOS 700D",
                Type = new string[] { "Canon EOS 700D" },
                SupportedModel = new string[] { "Canon EOS 700D" }
            });

            list.Add(new DeviceDto()
            {
                Name = "Canon EOS 1100D",
                DisplayName = "Canon EOS 1100D",
                Type = new string[] { "Canon EOS 1100D" },
                SupportedModel = new string[] { "Canon EOS 1100D" }
            });

            //list.Add(new DeviceDto()
            //{
            //    Name = "Canon EOS 4000D",
            //    DisplayName = "Canon EOS 4000D",
            //    Type = new string[] { "Canon EOS 4000D" },
            //    SupportedModel = new string[] { "Canon EOS 4000D" }
            //});


            //list.Add(new DeviceDto()
            //{
            //    DisplayName = "HP HD Web Cam",
            //    Name = "HP HD Webcam",
            //    Type = new string[] { "HP HD Webcam" },
            //    SupportedModel = new string[] { "HP HD Webcam" }
            //});

            //list.Add(new DeviceDto()
            //{
            //    DisplayName = "UVC Web Cam",
            //    Name = "UVC WebCam",
            //    Type = new string[] { "UVC WebCam" },
            //    SupportedModel = new string[] { "UVC WebCam" }
            //});

            return list;
        }

        public static List<DeviceDto> GetFpDeviceList()
        {

            List<DeviceDto> list = new List<DeviceDto>();
            list.Add(new DeviceDto()
            {
                DisplayName = "Futronic Fingerprint Scanner Device FS50",
                Name = "Futronic",
                Type = new string[] { "Futronic" },
                SupportedModel = new string[] { "Futronic" }
            });

            list.Add(new DeviceDto()
            {
                DisplayName = "Cross Match Technologies VERIFIER 320LC",
                Name = "Cross Match Technologies VERIFIER 320LC",
                Type = new string[] { "Cross Match Technologies VERIFIER 320LC" },
                SupportedModel = new string[] { "Cross Match Technologies VERIFIER 320LC" }
            });

            return list;
        }

        public static List<DeviceDto> GetIrisDeviceList()
        {

            List<DeviceDto> list = new List<DeviceDto>();

            list.Add(new DeviceDto()
            {
                Name = "Cross Match Technologies USB 2.0 Iris scanner",
                Type = new string[] { "Cross Match Technologies USB 2.0 Iris scanner" },
                SupportedModel = new string[] { "Cross Match Technologies USB 2.0 Iris scanner" }
            });

            list.Add(new DeviceDto()
            {
                Name = "IriMagic 1000BK",
                Type = new string[] { "IriMagic 1000BK" },
                SupportedModel = new string[] { "IriMagic 1000BK" }
            });

            list.Add(new DeviceDto()
            {
                Name = "IriShield BK B2121U",
                Type = new string[] { "IriShield B2121" },
                SupportedModel = new string[] { "IriShield B2121" }
            });

            list.Add(new DeviceDto()
            {
                Name = "CROSSMATCH USB2.0 Camera (5.0M Monochrome)",
                Type = new string[] { "CROSSMATCH USB2.0 Camera (5.0M Monochrome)" },
                SupportedModel = new string[] { "CROSSMATCH USB2.0 Camera (5.0M Monochrome)" }
            });

            return list;
        }
    }
}
