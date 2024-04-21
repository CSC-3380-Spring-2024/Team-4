extends Control

@onready var travPort = $"Travel-Animation"
@onready var batPort = $"Quick-Battle-Animation"




# Called when the node enters the scene tree for the first time.
func _ready():
	travPort.play("Travel-Portal")
	batPort.play("Quick-Battle-Portal")
	$"[E]xit".hide()
	$"[E]nter-qb-portal".hide()
	$"[E]nter-travel-portal".hide()



func _on_map_pressed():
	get_tree().change_scene_to_file("res://scenes/map/map.tscn");


func _on_back_pressed():
	get_tree().change_scene_to_file("res://scenes/menu/menu.tscn")


func _on_deck_pressed():
	get_tree().change_scene_to_file("res://Cards/cryptdex.tscn");




func _on_area_2d_body_entered(body):
	$"[E]xit".show()
	if Input.is_key_pressed(KEY_E):
		get_tree().change_scene_to_file("res://scenes/menu/menu.tscn")
