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

sizex = 5
sizey = 5
level = Map(sizex,sizey)
for i in range(sizex):
  level.mapMatrix[i][0] = cell_map_type[mountain]
  level.mapMatrix[i][sizey-1] =cell_map_type[mountain]

for i in range(sizey):
  level.mapMatrix[0][i] = cell_map_type[mountain]
  level.mapMatrix[sizex-1][i] = cell_map_type[mountain]

level.mapMatrix[1][3] = cell_map_type[outpost]
for i in range(2):
  level.mapMatrix[2][2+i] = cell_map_type[river]

level.mapMatrix[2][1] = cell_map_type[woods]
level.mapMatrix[3][1] = cell_map_type[woods]
level.mapMatrix[3][2] = cell_map_type[woods]
level.mapMatrix[3][3] = cell_map_type[woods]

level.mapMatrix[2][1] = cell_map_type[1]

#We print the map
for i in range(sizex):
  for j in range(sizey):
    print(level.mapMatrix[i][j][0],end = " - ")
  print()


with open('level2.json', 'w') as outfile:
    json.dump(level.__dict__, outfile)
