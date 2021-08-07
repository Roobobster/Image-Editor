# Image-Editor
A Image editor project that allows you to create new images based on a given image. Used to generate new images to train my hand coded neural network. It's not the most efficient algorithm but I didn't need speed and it gets the job done. I only need it to run once therefore the speed is only made for that scenario. 

## Edit Options
  - RGBA Options
    - Red values only
    - Blue values only
    - Green values only
    - Alpha representation in black in white
    - Primary RGB value only 
  - Contrast Options
    - Gray scale 
    - Vertical contrast
    - Horizontal contrast
    - Circle contrast
  - Blue Options
    - Horizontal Blur
    - Vertical Blur
    - Circle Blur

## Example Outputs
![Example](/readmeImages/example.png)


## Recommended Addition For Performance 
Best thing to add is to first scale the image down to make it run quicker. Also adding multi threading is also needed if you want to use it at run time or large amounts of images. Also improving some of the algorithms would make it faster as a few are at run times of O(N^3) which is extremely slow and some maybe even worse.
