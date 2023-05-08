using System;

public interface ISunbed
{
    float Price {
        get;
        set;
    }

    float FillAmount {
        get;
        set;
    }

    float GivenMoney {
        get;
        set;
    }

    void ITrigger();
}
