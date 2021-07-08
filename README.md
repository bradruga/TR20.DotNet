# TR20.DotNet

DotNet wrapper for TR20 computation engine.

## Installation

This package relies on the TR20.exe engine.  The engine is included in the WinTR-55 installation.

[https://www.nrcs.usda.gov/wps/portal/nrcs/detailfull/national/water/?cid=stelprdb1042901](https://www.nrcs.usda.gov/wps/portal/nrcs/detailfull/national/water/?cid=stelprdb1042901)

## Usage
```
# Create input object
var input = new TR20Input();
# ...

# Initialize engine.  Give the path to the folder containing TR20.exe.  If the executable is the root directory of your application, no need to give a path.
var engine = new TR20Engine(@"C:\Program Files (x86)\USDA\WinTR-55");

# Run engine and get output
var output = engine.Run(input);

```