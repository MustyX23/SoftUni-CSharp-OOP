namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using System;
    using System.Text;

    public class Tests
    {
        private Device device;
        [SetUp]
        public void Setup()
        {
            device = new Device(50);
            //memorycapacity int

            //this.AvailableMemory = memoryCapacity;
            //this.Photos = 0;
            //this.Applications = new List<string>();
        }

        [Test]
        public void Constructor_Properly_Sets_Values()
        {
            Assert.AreEqual(50, device.MemoryCapacity);
            Assert.AreEqual(device.MemoryCapacity, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(0, device.Applications.Count);
        }
        [Test]
        [TestCase(30, 20)]
        [TestCase(50, 0)]
        public void TakePhoto_Takes_Photo_Returns_True(int photoSize, int expected)
        {
            Assert.IsTrue(device.TakePhoto(photoSize));            
            Assert.AreEqual(expected, device.AvailableMemory);
            Assert.AreEqual(1, device.Photos);            
        }

        [Test]
        [TestCase(51)]        
        public void TakePhoto_Takes_Photo_Returns_False(int photoSize)
        {
            Assert.IsFalse(device.TakePhoto(photoSize));                  
        }

        [Test]
        [TestCase(30, 20)]
        [TestCase(50, 0)]
        public void TestInstallAppSuccess(int appSize, int expected)
        {
            string result = device.InstallApp("TestApp", appSize);
            Assert.AreEqual("TestApp is installed successfully. Run application?", result);
            Assert.AreEqual(expected, device.AvailableMemory);
            Assert.AreEqual(1, device.Applications.Count);
            Assert.AreEqual("TestApp", device.Applications[0]);
        }

        [Test]
        public void TestInstallAppFail()
        {
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("TestApp", 100));
            Assert.AreEqual(50, device.AvailableMemory);
            Assert.AreEqual(0, device.Applications.Count);
        }

        [Test]
        public void TestFormatDevice()
        {
            device.TakePhoto(20);
            device.InstallApp("TestApp", 10);

            device.FormatDevice();

            Assert.AreEqual(50, device.AvailableMemory);
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(0, device.Applications.Count);
        }
        [Test]
        public void TestGetDeviceStatus()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Memory Capacity: 50 MB, Available Memory: 50 MB");
            result.AppendLine("Photos Count: 0");
            result.AppendLine("Applications Installed: ");

            string expected = result.ToString().TrimEnd();
            string actual = device.GetDeviceStatus();
            Assert.AreEqual(expected, actual);

            device.TakePhoto(10);
            result.Clear();
            result.AppendLine("Memory Capacity: 50 MB, Available Memory: 40 MB");
            result.AppendLine("Photos Count: 1");
            result.AppendLine("Applications Installed: ");

            expected = result.ToString().TrimEnd();
            actual = device.GetDeviceStatus();
            Assert.AreEqual(expected, actual);

            device.InstallApp("TestApp", 20);
            result.Clear();
            result.AppendLine("Memory Capacity: 50 MB, Available Memory: 20 MB");
            result.AppendLine("Photos Count: 1");
            result.AppendLine("Applications Installed: TestApp");

            expected = result.ToString().TrimEnd();
            actual = device.GetDeviceStatus();
            Assert.AreEqual(expected, actual);

            device.FormatDevice();
            result.Clear();
            result.AppendLine("Memory Capacity: 50 MB, Available Memory: 50 MB");
            result.AppendLine("Photos Count: 0");
            result.AppendLine("Applications Installed: ");

            expected = result.ToString().TrimEnd();
            actual = device.GetDeviceStatus();
            Assert.AreEqual(expected, actual);
        }
    }
}    
