# World generation

Implementation of a procedural voxel-world.

# Purpose
Recreate a basic world generation that include:
  - The orography is "random", but have mountains, hills and plains.
  - Seas and lakes
  - Snow
  - Seeds to generate the world

# Theory

To achieve this we use a "Perlin Noise map", that looks like this:

[IMAGE of PERLIN NOISE]

This is a representation of a coherent noise where the changes occurs gradually. Each point take a representation between 0 (white) to 1 (black), and each color in the middle. If we take a representation in 2D, we will have the graph of a function, with highs and lows and rounded peaks. 

[IMAGE 2D GRAPH]

This is not so close to the real world, so we will play with the amplitude and the frequency of different perlin noise maps to create different situations. For example, we can have 3 "octaves" (each PN) where the first one is the main shape of the mountain, the second one, gives more variety to the hillsides, and the last one, add more details with small rocks, for example.

For this, we will need something to regulate the the frequency of the octaves and his influence in the final result.

[IMAGE GRAPHS WITH LACUNARITY]

For the first problem, we will use the term _lacunarity_, a number that will multiply the function with a power higher as detailed we want it to be. For example, we can multiply the lacunarity power 0 for the main shape, power 1 for the hills and power 2 for the small rocks. This will create rounded shapes for the first one and higher and more peaked for the last one.

[IMAGE GRAPHS WITH PERSISTANCE]

Now, we should aggregate the octaves, but they should influence the final result differently, his _persistance_. With this number, we will reduce the amplitude of each one, giving a higher amplitude to the first one than the last one.

So, if we increase the lacunarity, more small features will appear, and with more persistance, more influence in the overall result.