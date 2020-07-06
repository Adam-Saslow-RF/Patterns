public enum StateChangeOptions
{
    Update  = 1,
    Notify  = 1 << 1,
    UpdateAndNotify = Update | Notify
};