# Video
<a href="https://youtu.be/VOrHfnLJSbk">
  <img src="README_Images/ASE_title.png" width="50%">
</a>

# Requirements
## IDE
Unity 2022.3.12f1

# How to Use BiFuzz
1. Store the 'BiFuzz' directory in the 'Assets' directory of the target video game and open the project in Unity Editor.
2. Attach all the scripts in 'BiFuzz' directory to a player's character object.
3. When you attach a script, it becomes a component. In the added component, uncheck 'Grad_init' and 'Grad_local' to disable them.
4. Set the 'Grad_init' and 'Grad_local' components of the player's character object as shown in the image below. Even if there is no change from initial values, be sure to move sliders and re-enter the values.
    <img src="README_Images/Grad_init.png" width="50%">
    <img src="README_Images/Grad_local.png" width="50%">
5. Assign the number of frames the test will run to the variable exeTime in Assets/BiFuzz/ExeTime.cs.
6. Empty Assets/Logs and run the scene. Test results will be saved as a CSV file in Assets/Logs.

# Directory Structure
<pre>
BiFuzz  
├── BiFuzz
└── StarCollection
    └── Assets
</pre>

The default play style is Play Style A introduced in the paper. To change the play style, rewrite the 'MutateParam' method in 'bifuzz_init.cs' and 'bifuzz_local.cs'.

# StarCollection
This is the target video game applying BiFuzz in the experiment of the paper.
## Required Assets
- [Dog Knight PBR Polyart](https://assetstore.unity.com/packages/3d/characters/animals/dog-knight-pbr-polyart-135227)
- [Fantasy Worlds: Forest FREE - Stylized Forest Environment Open World](https://assetstore.unity.com/packages/3d/environments/fantasy/fantasy-worlds-forest-free-stylized-forest-environment-open-worl-282610)
- [AllSky Free - 10 Sky / Skybox Set](https://assetstore.unity.com/packages/2d/textures-materials/sky/allsky-free-10-sky-skybox-set-146014)
- [Fantasy Forest Environment - Free Demo](https://assetstore.unity.com/packages/3d/environments/fantasy/fantasy-forest-environment-free-demo-35361)
- [Simple Gems and Items Ultimate Animated Customizable Pack](https://assetstore.unity.com/packages/3d/props/simple-gems-and-items-ultimate-animated-customizable-pack-73764) (The latest version is unapplicable, so the old version is included in this repository.)