#https://www.onlinegdb.com/online_python_compiler
import json

cell_map_type = ["Plain", "River", "Woods", "Mountain", "Outpost"]
plain = 0
river = 1
woods = 2
mountain = 3
outpost = 4

class Map:
	mapSize =[]
	mapMatrix = [];

	def __init__(self, x, y):
		self.mapSize = [x,y]
		self.mapMatrix = [[0 for i in range(x)] for j in range(y)]
		for i in range(x):
			for j in range(y):
				self.mapMatrix[i][j] = cell_map_type[0]

sizex = 10
sizey = 10
level = Map(sizex,sizey)

#We fill the borders with mountains
for i in range(sizex):
  level.mapMatrix[i][0] = cell_map_type[mountain]
  level.mapMatrix[i][sizey-1] =cell_map_type[mountain]
for i in range(sizey):
  level.mapMatrix[0][i] = cell_map_type[mountain]
  level.mapMatrix[sizex-1][i] = cell_map_type[mountain]

#We create the outpost zone
level.mapMatrix[8][7] = cell_map_type[outpost]
level.mapMatrix[8][8] = cell_map_type[outpost]
level.mapMatrix[7][8] = cell_map_type[outpost]

#We create the river
for i in range(3):
  level.mapMatrix[7+i][3] = cell_map_type[river]
level.mapMatrix[7][4] = cell_map_type[river]
for i in range(3):
    level.mapMatrix[6][4+i] = cell_map_type[river]
for i in range(7):
    level.mapMatrix[i][6] = cell_map_type[river]

#We put some woods
level.mapMatrix[4][6] = cell_map_type[woods]
level.mapMatrix[5][6] = cell_map_type[woods]

#We put two more random mountains
level.mapMatrix[3][3] = cell_map_type[mountain]
level.mapMatrix[7][2] = cell_map_type[mountain]


#We print the map
for i in range(sizex):
  for j in range(sizey):
    print(level.mapMatrix[i][j][0],end = " - ")
  print()


with open('level3.json', 'w') as outfile:
    json.dump(level.__dict__, outfile)
