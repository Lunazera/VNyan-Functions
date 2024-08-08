Calculates Mean, Variance and Standard Deviation of a window of 30 samples (data saved each frame) every 30 frames.

### How to use:
- Drop the dll and customobject into VNyan's Assemblies folder
- Listens for data in the specific parameter `VNyanMath_Stats_Input`. Whatever you load into this parameter will enter the calculation.
- Outputs calculation in `VNyanMath_Mean`, `VNyanMath_Var`, and `VNyanMath_STD`

### Use cases: 
Checking parameters or blendshapes for any unexpected things or irregularities. Like, say you want to see if a blendshape is working properly and how stable it is, save that blendshape into the input parameter and the mean will tell you what average value it's hitting, while the variance and standard deviation will tell you if it's generally stable (low values) or if it's unstable or messy/jittery (high values).

Note that right now it just runs constantly, so you probably don't want to keep the files around unless you're using them.

At some point I'll update this to have some actual controls within a plugin window.
