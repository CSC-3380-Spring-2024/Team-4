# Map functionality courtesy of GodotGameLab on youtube

# This class is for handling each individual "Room node" on the map - hence the class_name Room
# The whole map is made up of several Room nodes.
class_name Room
extends Resource

# Enumerations for room type
enum Type {NOT_ASSIGNED, MONSTER, TREASURE, SHOP, BOSS}

# Important data that every room node needs to have saved
@export var type: Type
@export var row: int
@export var column: int
@export var position: Vector2
@export var next_rooms: Array[Room]
@export var selected:= false
