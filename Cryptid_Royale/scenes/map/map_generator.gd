# Map functionality courtesy of GodotGameLab on youtube

# This class generates the "backend" of the map, such as the data behind the map itself
class_name MapGenerator
extends Node

# Constant variables for the map generator, feel free to change values
const X_DISTANCE := 30
const Y_DISTANCE := 25
const NODE_PLACEMENT_RANDOMNESS := 5
const NUM_FLOORS := 15
const NODES_PER_FLOOR := 7
const MAX_PATHS := 6
const MONSTER_ROOM_WEIGHT := 10.0
const SHOP_ROOM_WEIGHT := 2.5

# Not sure what this does yet but the tutorial guy said to add it
# Will probably learn later
var random_room_type_weights = {
	Room.Type.MONSTER: 0.0,
	Room.Type.SHOP: 0.0,
}
var random_room_type_total_weight := 0

# The map data will be saved as an array of arrays (a matrix) of Rooms
var map_data: Array[Array]

# Generates the map
func generate_map() -> Array[Array]:
	map_data = _generate_initial_grid()
	var starting_points := _get_random_starting_points()
	
	# Generates all of the links between Room nodes
	for j in starting_points:
		var current_j := j
		for i in NUM_FLOORS - 1:
			current_j = _setup_connection(i, current_j)
	
	_setup_boss_room()
	_setup_random_room_weights()
	_setup_room_types()
	
	return map_data

# Creates the initial grid to be molded into the game's internal map
func _generate_initial_grid() -> Array[Array]:
	
	var result: Array[Array]
	result = []
	
	# Creates and populates an array for every floor
	for i in NUM_FLOORS:
		var current_floor: Array[Room] = []
		
		# Puts a room in every available spot for each given floor
		for j in NODES_PER_FLOOR:
			var new_room:= Room.new()
			var offset := Vector2(randf(), randf()) * NODE_PLACEMENT_RANDOMNESS
			new_room.position = Vector2(j * X_DISTANCE, i * -Y_DISTANCE) + offset
			new_room.row = i
			new_room.column = j
			new_room.next_rooms = []
			
			# Boss room extra space
			if i == NUM_FLOORS - 1:
				new_room.position.y = (i + 1) * -Y_DISTANCE
			
			current_floor.append(new_room)
			
		result.append(current_floor)
		
	return result


# Generates the starting nodes for the map
func _get_random_starting_points() -> Array[int]:
	
	var start_positions: Array[int]
	var unique_points: int = 0
	
	# Repeats generation until at least 2 unique numbers
	while unique_points < 2:
		
		unique_points = 0
		start_positions = []
		
		# Chooses random numbers to be the starting nodes
		for i in MAX_PATHS:
			var new_start_position := randi_range(0, NODES_PER_FLOOR - 1)
			if not start_positions.has(new_start_position):
				unique_points += 1
			
			start_positions.append(new_start_position)
	
	return start_positions


# Sets up connections between map nodes
func _setup_connection(i: int, j: int) -> int:
	var next_room: Room
	var current_room := map_data[i][j] as Room
	
	while not next_room or _would_cross_existing_path(i, j, next_room):
		var random_j := clampi(randi_range(j - 1, j + 1), 0, NODES_PER_FLOOR - 1)
		next_room = map_data[i + 1][random_j]
	
	current_room.next_rooms.append(next_room)
	
	return next_room.column


# Checks if possible next path would cross an existing path
func _would_cross_existing_path(i: int, j: int, room: Room) -> bool:
	var left_neighbor: Room
	var right_neighbor: Room
	
	# Establish if there is a left neighbor
	if j > 0:
		left_neighbor = map_data[i][j - 1]
	
	# Establish if there is a right neighbor
	if j < NODES_PER_FLOOR - 1:
		right_neighbor = map_data[i][j + 1]
	
	# Check if current next room will cross a path with right neighbor
	if right_neighbor and room.column > j:
		for next_room: Room in right_neighbor.next_rooms:
			if next_room.column < room.column:
				return true
	
	# Check if current next room will cross a path with left neighbor
	if left_neighbor and room.column < j:
		for next_room: Room in left_neighbor.next_rooms:
			if next_room.column > room.column:
				return true
	
	return false


# Handles the boss room, making sure all penultimate nodes lead to the final battle
func _setup_boss_room() -> void:
	var middle := floori(NODES_PER_FLOOR * 0.5)
	var boss_room := map_data[NUM_FLOORS - 1][middle] as Room
	
	for j in NODES_PER_FLOOR:
		var current_room = map_data[NUM_FLOORS - 2][j] as Room
		if current_room.next_rooms:
			current_room.next_rooms = [] as Array[Room]
			current_room.next_rooms.append(boss_room)
	
	boss_room.type = Room.Type.BOSS


# Copied from tutorial, don't quite understand, something to do with randomizing the room rates
func _setup_random_room_weights() -> void:
	random_room_type_weights[Room.Type.MONSTER] = MONSTER_ROOM_WEIGHT
	random_room_type_weights[Room.Type.SHOP] = SHOP_ROOM_WEIGHT + MONSTER_ROOM_WEIGHT
	
	random_room_type_total_weight = random_room_type_weights[Room.Type.SHOP]


# Handles room types
func _setup_room_types() -> void:
	# Guarantees first floor always a battle
	for room: Room in map_data[0]:
		if room.next_rooms.size() > 0:
			room.type = Room.Type.MONSTER
	
	# Guarantees at least one shop in every path about halfway up the map
	for room: Room in map_data[floori(NUM_FLOORS * 0.5)]:
		if room.next_rooms.size() > 0:
			room.type = Room.Type.SHOP
	
	# Randomizes the rest of the rooms
	for current_floor in map_data:
		for room: Room in current_floor:
			for next_room: Room in room.next_rooms:
				if next_room.type == Room.Type.NOT_ASSIGNED:
					_set_room_randomly(next_room)


func _set_room_randomly(room_to_set: Room) -> void:
	var consecutive_shop = true
	
	var type_candidate: Room.Type
	
	while consecutive_shop:
		type_candidate = _get_random_room_type_by_weight()
		
		var is_shop = type_candidate == Room.Type.SHOP
		var has_shop_parent := _room_has_parent_of_type(room_to_set, Room.Type.SHOP)
		
		consecutive_shop = is_shop and has_shop_parent
		
	room_to_set.type = type_candidate


func _room_has_parent_of_type(room: Room, type: Room.Type) -> bool:
	var parents: Array[Room] = []
	
	# Left parent
	if room.column > 0 and room.row > 0:
		var parent_candidate := map_data[room.row - 1][room.column - 1] as Room
		if parent_candidate.next_rooms.has(room):
			parents.append(parent_candidate)
	
	# Middle parent
	if room.row > 0:
		var parent_candidate := map_data[room.row - 1][room.column] as Room
		if parent_candidate.next_rooms.has(room):
			parents.append(parent_candidate)
	
	# Right parent
	if room.column < NODES_PER_FLOOR - 1 and room.row > 0:
		var parent_candidate := map_data[room.row - 1][room.column + 1] as Room
		if parent_candidate.next_rooms.has(room):
			parents.append(parent_candidate)
	
	for parent: Room in parents:
		if parent.type == type:
			return true
	
	return false

# Integrates _setup_room_types and the random room type variables instantiated on lines 17-20
func _get_random_room_type_by_weight() -> Room.Type:
	var roll := randf_range(0.0, random_room_type_total_weight)
	
	for type: Room.Type in random_room_type_weights:
		if random_room_type_weights[type] > roll:
			return type
	
	return Room.Type.MONSTER
