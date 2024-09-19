using System.Diagnostics;
using System.Windows.Forms;
using ABT.Test.TestExecutive.SCPI_VISA_Instruments.Multifunction;
using ABT.Test.TestExecutive.Switching;
using static ABT.Test.TestExecutive.SCPI_VISA_Instruments.Multifunction.MSMU_34980A;
using static ABT.Test.TestExecutive.SCPI_VISA_Instruments.SCPI_VISA_Instrument;

namespace TestExecTests.SCPI_VISA_Instruments.Multifunction;

[TestClass()]
public class MSMU_34980ATests
{

    internal static MSMU_34980A MSMU = new(new Alias("MSMU1"), "Agilent E3649A Dual Ouput DC Power Supply", "TCPIP0::10.25.32.13::INSTR", "MSMU_34980A");
    internal const String FIRST_34921A = "@1001:1040";
    internal const String ALL_34921A = "@1001:5040";
    internal const String ABUS1 = "@1911,1921,2911,2921,3911,3921,4911,4921,5911,5921";
    internal const String ABUS2 = "@1912,1922,2912,2922,3912,3922,4912,4922,5912,5922";
    internal const String ABUS3 = "@1913,1923,2913,2923,3913,3923,4913,4923,5913,5923";
    internal const String ABUS4 = "@1914,1924,2914,2924,3914,3924,4914,4924,5914,5924";

    [TestMethod()]
    public void DiagnosticRelayCyclesTest()
    {
        List<Int32> relayCycles;
        relayCycles = MSMU.DiagnosticRelayCycles(FIRST_34921A);
        Assert.AreEqual(40, relayCycles.Count);
        relayCycles = MSMU.DiagnosticRelayCycles("@6001:6064");
        Assert.AreEqual(64, relayCycles.Count);
    }

    [TestMethod()]
    public void InstrumentDMM_InstalledTest()
    {
        Assert.IsTrue(MSMU.InstrumentDMM_Installed());
    }

    [TestMethod()]
    public void InstrumentDMM_GetTest()
    {
        // Tested in InstrumentDMM_SetTest().
    }

    [TestMethod()]
    public void InstrumentDMM_SetTest()
    {
        MSMU.InstrumentDMM_Set(STATES.off);
        Assert.AreEqual(STATES.off, MSMU.InstrumentDMM_Get());
        MSMU.InstrumentDMM_Set(STATES.ON);
        Assert.AreEqual(STATES.ON, MSMU.InstrumentDMM_Get());
    }

    [TestMethod()]
    public void ModuleChannelsTest()
    {
        Assert.AreEqual((1, 44), MSMU.ModuleChannels(SLOTS.Slot1));
        Assert.AreEqual((1, 44), MSMU.ModuleChannels(SLOTS.Slot2));
        Assert.AreEqual((1, 44), MSMU.ModuleChannels(SLOTS.Slot3));
        Assert.AreEqual((1, 44), MSMU.ModuleChannels(SLOTS.Slot4));
        Assert.AreEqual((1, 44), MSMU.ModuleChannels(SLOTS.Slot5));
        Assert.AreEqual((1, 68), MSMU.ModuleChannels(SLOTS.Slot6));
        Assert.AreEqual((1, 68), MSMU.ModuleChannels(SLOTS.Slot7));
        Assert.AreEqual((1, 7), MSMU.ModuleChannels(SLOTS.Slot8));
    }

    [TestMethod()]
    public void RouteCloseExclusiveTest()
    {
        MSMU.Command(COMMANDS.RST); // Opens all relays.
        Assert.IsTrue(MSMU.RouteGet(FIRST_34921A, RELAY_STATES.opened));
        MSMU.RouteCloseExclusive("@1001:1005");
        Assert.IsTrue(MSMU.RouteGet("@1001:1005", RELAY_STATES.CLOSED));
        Assert.IsTrue(MSMU.RouteGet("@1006:1040", RELAY_STATES.opened));
    }

    [TestMethod()]
    public void RouteOpenABUS()
    {
        if (MessageBox.Show("Do you have Interlock enabled on Slots 1 through Slots 5?", "Interlock", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        {
            Assert.Inconclusive();
            return;
        }

        MSMU.Command(COMMANDS.RST); // Opens all relays.
        Assert.IsTrue(MSMU.RouteGet(ABUS1, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet(ABUS2, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet(ABUS3, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet(ABUS4, RELAY_STATES.opened));

        MSMU.RouteSet(ABUS1, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ABUS1, RELAY_STATES.CLOSED));
        MSMU.RouteOpenABUS(ABUS.ABUS1);
        Assert.IsTrue(MSMU.RouteGet(ABUS1, RELAY_STATES.opened));

        MSMU.RouteSet(ABUS2, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ABUS2, RELAY_STATES.CLOSED));
        MSMU.RouteOpenABUS(ABUS.ABUS2);
        Assert.IsTrue(MSMU.RouteGet(ABUS2, RELAY_STATES.opened));

        MSMU.RouteSet(ABUS3, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ABUS3, RELAY_STATES.CLOSED));
        MSMU.RouteOpenABUS(ABUS.ABUS3);
        Assert.IsTrue(MSMU.RouteGet(ABUS3, RELAY_STATES.opened));

        MSMU.RouteSet(ABUS4, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ABUS4, RELAY_STATES.CLOSED));
        MSMU.RouteOpenABUS(ABUS.ABUS4);
        Assert.IsTrue(MSMU.RouteGet(ABUS4, RELAY_STATES.opened));

        MSMU.RouteSet(ABUS1, RELAY_STATES.CLOSED);
        MSMU.RouteSet(ABUS2, RELAY_STATES.CLOSED);
        MSMU.RouteSet(ABUS3, RELAY_STATES.CLOSED);
        MSMU.RouteSet(ABUS4, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ABUS1, RELAY_STATES.CLOSED));
        Assert.IsTrue(MSMU.RouteGet(ABUS2, RELAY_STATES.CLOSED));
        Assert.IsTrue(MSMU.RouteGet(ABUS3, RELAY_STATES.CLOSED));
        Assert.IsTrue(MSMU.RouteGet(ABUS4, RELAY_STATES.CLOSED));

        MSMU.RouteOpenABUS(ABUS.ALL);
        Assert.IsTrue(MSMU.RouteGet(ABUS1, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet(ABUS2, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet(ABUS3, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet(ABUS4, RELAY_STATES.opened));
    }

    [TestMethod()]
    public void RouteOpenAllSlotTest()
    {
        MSMU.RouteSet(ALL_34921A, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ALL_34921A, RELAY_STATES.CLOSED));
        MSMU.RouteOpenAllSlot(SLOTS.Slot1);
        Assert.IsTrue(MSMU.RouteGet(FIRST_34921A, RELAY_STATES.opened));
        Assert.IsTrue(MSMU.RouteGet("@2001:5040", RELAY_STATES.CLOSED));
    }

    [TestMethod()]
    public void RouteOpenAllTest()
    {
        MSMU.RouteSet(ALL_34921A, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(ALL_34921A, RELAY_STATES.CLOSED));
        MSMU.RouteOpenAll();
        Assert.IsTrue(MSMU.RouteGet(ALL_34921A, RELAY_STATES.opened));
    }

    [TestMethod()]
    public void RouteGetTest()
    {
        MSMU.RouteSet(ALL_34921A, RELAY_STATES.opened);
        Assert.IsTrue(MSMU.RouteGet(ALL_34921A, RELAY_STATES.opened));

        const String most_34921A = "@1001:3040,4020,5001:5040";
        MSMU.RouteSet(most_34921A, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(most_34921A, RELAY_STATES.CLOSED));

        const String all_except_most_34921A = "@4001:4019,4021:4040";
        Assert.IsTrue(MSMU.RouteGet(all_except_most_34921A, RELAY_STATES.opened));
    }

    [TestMethod()]
    public void RouteSetTest()
    {
        // Tested in RouteGetTest().
    }

    [TestMethod()]
    public void SystemABusInterlockSimulateGetTest()
    {
        // Tested in SystemABusInterlockSimulateSetTest().
    }

    [TestMethod()]
    public void SystemABusInterlockSimulateSetTest()
    {
        MSMU.SystemABusInterlockSimulateSet(STATES.ON);
        Assert.AreEqual(STATES.ON, MSMU.SystemABusInterlockSimulateGet());
        MSMU.SystemABusInterlockSimulateSet(STATES.off);
        Assert.AreEqual(STATES.off, MSMU.SystemABusInterlockSimulateGet());
    }

    [TestMethod()]
    public void SystemDateGetTest()
    {
        // Tested in SystemDateSetTest().
    }

    [TestMethod()]
    public void SystemDateSetTest()
    {
        DateOnly dset = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        MSMU.SystemDateSet(dset);
        DateOnly dget = MSMU.SystemDateGet();
        Debug.Print($"SystemDateSet='{dset.ToShortDateString()}', SystemDateGet='{dget.ToShortDateString()}'.");
        Assert.AreEqual(dset, dget);
    }

    [TestMethod()]
    public void SystemDescriptionLongTest()
    {
        Assert.AreEqual("40-Channel Armature Multiplexer with Low Thermal Offset", MSMU.SystemDescriptionLong(SLOTS.Slot1));
        Assert.AreEqual("40-Channel Armature Multiplexer with Low Thermal Offset", MSMU.SystemDescriptionLong(SLOTS.Slot2));
        Assert.AreEqual("40-Channel Armature Multiplexer with Low Thermal Offset", MSMU.SystemDescriptionLong(SLOTS.Slot3));
        Assert.AreEqual("40-Channel Armature Multiplexer with Low Thermal Offset", MSMU.SystemDescriptionLong(SLOTS.Slot4));
        Assert.AreEqual("40-Channel Armature Multiplexer with Low Thermal Offset", MSMU.SystemDescriptionLong(SLOTS.Slot5));
        Assert.AreEqual("64-Channel Form A General-Purpose Switch", MSMU.SystemDescriptionLong(SLOTS.Slot6));
        Assert.AreEqual("64-Channel Form A General-Purpose Switch", MSMU.SystemDescriptionLong(SLOTS.Slot7));
        Assert.AreEqual("Multifunction Module with DIO, D/A, and Totalizer", MSMU.SystemDescriptionLong(SLOTS.Slot8));
    }

    [TestMethod()]
    public void SystemModuleTemperatureTest()
    {
        Assert.IsInstanceOfType(MSMU.SystemModuleTemperature(SLOTS.Slot6), typeof(Double));                                 // SLOTS.Slot6 & Slot7 are valid 34939A modules.
        Assert.IsTrue(18 <= MSMU.SystemModuleTemperature(SLOTS.Slot7) && MSMU.SystemModuleTemperature(SLOTS.Slot7) <= 30);  // Degrees Centigrade.
    }

    [TestMethod()]
    public void SystemPresetTest()
    {
        MSMU.RouteSet(FIRST_34921A, RELAY_STATES.CLOSED);
        Assert.IsTrue(MSMU.RouteGet(FIRST_34921A, RELAY_STATES.CLOSED));
        MSMU.SystemPreset();
        Assert.IsTrue(MSMU.RouteGet(FIRST_34921A, RELAY_STATES.opened));
    }

    [TestMethod()]
    public void SystemTimeGetTest()
    {
        // Tested in SystemTimeSetTest().
    }

    [TestMethod()]
    public void SystemTimeSetTest()
    {
        TimeOnly tset = new(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        MSMU.SystemTimeSet(tset);
        TimeOnly tget = MSMU.SystemTimeGet();
        Debug.Print($"SystemTimeSet='{tset.ToShortTimeString()}', SystemTimeSet='{tget.ToShortTimeString()}'.");
        Assert.AreEqual(tset, tget);
    }

    [TestMethod()]
    public void SystemTypeTest()
    {
        Assert.AreEqual("34921A", MSMU.SystemType(SLOTS.Slot1));
        Assert.AreEqual("34921A", MSMU.SystemType(SLOTS.Slot2));
        Assert.AreEqual("34921A", MSMU.SystemType(SLOTS.Slot3));
        Assert.AreEqual("34921A", MSMU.SystemType(SLOTS.Slot4));
        Assert.AreEqual("34921A", MSMU.SystemType(SLOTS.Slot5));
        Assert.AreEqual("34939A", MSMU.SystemType(SLOTS.Slot6));
        Assert.AreEqual("34939A", MSMU.SystemType(SLOTS.Slot7));
        Assert.AreEqual("34952A", MSMU.SystemType(SLOTS.Slot8));
    }

    [TestMethod()]
    public void UnitsGetTest()
    {
        // Tested in UnitsGetTest().
    }

    [TestMethod()]
    public void UnitsSetTest()
    {
        MSMU.UnitsSet(TEMPERATURE_UNITS.C);
        Assert.AreEqual(TEMPERATURE_UNITS.C, MSMU.UnitsGet());
        MSMU.UnitsSet(TEMPERATURE_UNITS.F);
        Assert.AreEqual(TEMPERATURE_UNITS.F, MSMU.UnitsGet());
        MSMU.UnitsSet(TEMPERATURE_UNITS.K);
        Assert.AreEqual(TEMPERATURE_UNITS.K, MSMU.UnitsGet());
        MSMU.UnitsSet(TEMPERATURE_UNITS.C);
    }

}