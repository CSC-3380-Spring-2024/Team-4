# Map functionality courtesy of GodotGameLab on youtube
# ngl I don't understand how most of this class works

# This class handles the visuals of the map itself, 
# showing UI to the user based on the map_generator class
class_name MapRoom
extends Area2D

signal selected(room: Room)

const ICONS := {
	Room.Type.NOT_ASSIGNED: [null, Vector2.ONE],
	Room.Type.MONSTER: [preload("res://assets/map_sword.png"), Vector2.ONE],
	Room.Type.SHOP: [preload("res://assets/map_shop.png"), Vector2.ONE],
	Room.Type.BOSS: [preload("res://assets/map_boss.png"), Vector2(1.3, 1.3)]
}



@onready var sprite_2d: Sprite2D = $Visuals/Sprite2D
@onready var line_2d: Line2D = $Visuals/Line2D
@onready var animation_player: AnimationPlayer = $AnimationPlayer

var available := false : set = set_available
var room: Room : set = set_room


func set_available(new_value: bool) -> void:
	available = new_value
	
	if available:
		animation_player.play("highlight")
	elif not room.selected:
		animation_player.play("RESET")


func set_room(new_data: Room) -> void:
	room = new_data
	position = room.position
	sprite_2d.texture = ICONS[room.type][0]
	sprite_2d.scale = ICONS[room.type][1]


func show_selected() -> void:
	line_2d.modulate = Color.INDIAN_RED


func _on_input_event(viewport: Node, event: InputEventMouseButton, shape_idx: int) -> void:
	if not available or not event.is_action_pressed("left_click"):
		return
	
	room.selected = true
	animation_player.play("select")
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
	get_tree().change_scene_to_file("res://yetiMountainlevel/yetiMountain.tscn");
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
	get_tree().change_scene_to_file("res://yetiMountainlevel/yetiMountain.tscn");
>>>>>>> bce5a00c6e72334443f88009f9fd0e954d28dd38


# Called by AnimationPlayer when the "select" animation finishes
func _on_map_room_selected() -> void:
	selected.emit(room)











