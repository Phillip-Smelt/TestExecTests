using ABT.Test.TestExecutive.SCPI_VISA_Instruments.PowerSupplies.Keysight;
using static ABT.Test.TestExecutive.SCPI_VISA_Instruments.SCPI_VISA_Instrument;

namespace TestExecTests.SCPI_VISA_Instruments.PowerSupplies.Keysight;

[TestClass()]
public class PS_E3634ATests {
    internal static PS_E3634A PS = new(new Alias("PS1"), "Agilent E3634A Single Ouput Dual Range DC Power Supply", "GPIB0::3::INSTR", "PS_E3634A");

    [TestMethod()]
    public void SetTest() {
        Assert.Fail();
    }

    [TestMethod()]
    public void StateSetTest() {
        Assert.Fail();
    }

    [TestMethod()]
    public void SourceVoltageRangeGetTest() {
        Assert.Fail();
    }

    [TestMethod()]
    public void SourceVoltageRangeSetTest() {
        Assert.Fail();
    }
}