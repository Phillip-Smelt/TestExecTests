using ABT.Test.TestExecutive.SCPI_VISA_Instruments.PowerSupplies.Keysight;
using static ABT.Test.TestExecutive.SCPI_VISA_Instruments.SCPI_VISA_Instrument;
using static ABT.Test.TestExecutive.SCPI_VISA_Instruments.PowerSupplies.PS_OutputSingleRangeDual;


namespace TestExecTests.SCPI_VISA_Instruments.PowerSupplies.Keysight;

[TestClass()]
public class PS_E3634ATests {
    internal static PS_E3634A PS = new(new Alias("PS1"), "Agilent E3634A Single Ouput Dual Range DC Power Supply", "GPIB0::3::INSTR", "PS_E3634A");
    internal readonly Single Δ = Math.Abs(Single.MinValue);

    [TestMethod()]
    public void InLimits() {
        Assert.IsTrue(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.CURRent, PS.Limits[RANGE.v_low_HIGH_A][UNITS.CURRent][LIMITS.MINimum]));
        Assert.IsTrue(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.CURRent, PS.Limits[RANGE.v_low_HIGH_A][UNITS.CURRent][LIMITS.MAXimum]));
        Assert.IsTrue(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.VOLTage, PS.Limits[RANGE.v_low_HIGH_A][UNITS.VOLTage][LIMITS.MINimum]));
        Assert.IsTrue(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.VOLTage, PS.Limits[RANGE.v_low_HIGH_A][UNITS.VOLTage][LIMITS.MAXimum]));

        Assert.IsFalse(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.CURRent, PS.Limits[RANGE.v_low_HIGH_A][UNITS.CURRent][LIMITS.MINimum] - Δ));
        Assert.IsFalse(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.CURRent, PS.Limits[RANGE.v_low_HIGH_A][UNITS.CURRent][LIMITS.MAXimum] + Δ));
        Assert.IsFalse(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.VOLTage, PS.Limits[RANGE.v_low_HIGH_A][UNITS.VOLTage][LIMITS.MINimum] - Δ));
        Assert.IsFalse(PS.InLimits(RANGE.v_low_HIGH_A, UNITS.VOLTage, PS.Limits[RANGE.v_low_HIGH_A][UNITS.VOLTage][LIMITS.MAXimum] + Δ));

        Assert.IsTrue(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.CURRent, PS.Limits[RANGE.V_HIGH_low_a][UNITS.CURRent][LIMITS.MINimum]));
        Assert.IsTrue(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.CURRent, PS.Limits[RANGE.V_HIGH_low_a][UNITS.CURRent][LIMITS.MAXimum]));
        Assert.IsTrue(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.VOLTage, PS.Limits[RANGE.V_HIGH_low_a][UNITS.VOLTage][LIMITS.MINimum]));
        Assert.IsTrue(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.VOLTage, PS.Limits[RANGE.V_HIGH_low_a][UNITS.VOLTage][LIMITS.MAXimum]));

        Assert.IsFalse(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.CURRent, PS.Limits[RANGE.V_HIGH_low_a][UNITS.CURRent][LIMITS.MINimum] - Δ));
        Assert.IsFalse(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.CURRent, PS.Limits[RANGE.V_HIGH_low_a][UNITS.CURRent][LIMITS.MAXimum] + Δ));
        Assert.IsFalse(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.VOLTage, PS.Limits[RANGE.V_HIGH_low_a][UNITS.VOLTage][LIMITS.MINimum] - Δ));
        Assert.IsFalse(PS.InLimits(RANGE.V_HIGH_low_a, UNITS.VOLTage, PS.Limits[RANGE.V_HIGH_low_a][UNITS.VOLTage][LIMITS.MAXimum] + Δ));
    }

    [TestMethod()]
    public void SetRangeVoltsAmpsOVP_StateTest() {
        PS.Set(RANGE.v_low_HIGH_A, PS.Limits[RANGE.v_low_HIGH_A][UNITS.VOLTage][LIMITS.MAXimum], PS.Limits[RANGE.v_low_HIGH_A][UNITS.CURRent][LIMITS.MINimum], PS.Limits[RANGE.v_low_HIGH_A][UNITS.VOLTage][LIMITS.MAXimum], STATES.off);
        Assert.AreEqual(STATES.off, PS.GetState());
    }

    [TestMethod()]
    public void SetState() {
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