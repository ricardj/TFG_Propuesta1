import json

cell_map_type = ["Plain", "River", "Woods", "Mountain", "Outpost"]

class Map:
	mapSize =[]
	mapMatrix = [];

	def __init__(self, x, y):
		self.mapSize = [x,y]
		self.mapMatrix = [[0 for i in range(x)] for j in range(y)]
		for i in range(x):
			for j in range(y):
				self.mapMatrix[i][j] = cell_map_type[0]

level1 = Map(5,5)
level1.mapMatrix[2][1] = cell_map_type[1]
with open('level1.json', 'w') as outfile:
    json.dump(level1.__dict__, outfile)
