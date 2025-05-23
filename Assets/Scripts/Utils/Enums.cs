public enum TravelDirection{
    Left,
    Right,
    Back,
    Front
}

public enum SystemName
{
    None,
    // Station 1
    Lights,
    // Station 2
    Gates,
    Temperature,
    Oxygen,
    Asteroids,
    // Station 3
    Network,
    Coordinates,
    Shields,
    Quantum,
    // Station 4
    Fluids,
    Pressure,
    // Subsystems
    Subsystem
}

public enum StationName{
    None,
    Central,
    Navigation,
    Communication,
    Engineering,
    Entrance,
    Rest
}

public enum SelectEnums
{
    None,
    CoordinatesRecipients,
    FluidsNames
}

public enum CoordinatesRecipient
{
    Police,
    Hospital,
    Scientists,
    Government,
    CEO,
    Pizza
}

public enum FluidsName
{
    Water,
    Fuel,
    Cooling,
    Nuclear
}

public enum AsteroidCursorMovement
{
    Up,
    Down,
    Left,
    Right
}

public enum CursorType
{
    Open,
    Close,
    Circle,
    LeftRight,
    UpDown,
    Finger,
    Eye
}

public enum SwitchRowState
{
    Off,
    Partial,
    On
}
