WriteLine(Props["version"]);
WriteLine(Props.Get("configuration", "Release"));

// Some CI/CDs have integration of these properties.
// For example in TeamCity this property with all changes will be available in the next TeamCity steps.
Props["version"] = "1.1.6";
