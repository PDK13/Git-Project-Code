- Download package from Unity: Universal RP
- In Project: Create/Rendering/Universal Render Pipeline/Pipeline Asset (Forward Renderer), then renamed file.
- After renamed file, there are 2 file will be created. (*)
- In Project Settings: Graphics/Sciptable Render Pipeline Settings, add (*) file was created.


- In Project: Create/Shader/Universal Render Pipeline/Sprite Lit Shader Graph, then renamed file. (**)
- (**) file created with "LineDot" name is the completed file.

- In Scene: Create new emty GameObject, add Line Renderer Component.

+ In Line Renderer Component:
_ Chance Texture Mode to Tile.
_ Add Material (Create Material if not have one).

+ In Shader (Bottom of Inspector):
_ Chance Shader to Shader Graphs/"(**) file".

- To chance style of dot: Double Click on (**) file.

- To fix other Material (Default Material) get "Pink": In Project, chance all Material's Shader to Univeral Render Pipline/Lit.