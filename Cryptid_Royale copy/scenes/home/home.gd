extends Control

@onready var travPort = $"Travel-Animation"
@onready var batPort = $"Quick-Battle-Animation"
@onready var exit = $"Exit-door/Exit"
@onready var portal = $"Quick-Battle-Portal/Portal-Labels"
var flag = false



# Called when the node enters the scene tree for the first time.
func _ready():
	exit.hide()
	portal.hide()
	travPort.play("Travel-Portal")
	batPort.play("Quick-Battle-Portal")






func _on_deck_pressed():
	get_tree().change_scene_to_file("res://Cards/cryptdex.tscn");



func _on_area_2d_body_entered(body):
	exit.show()


func _on_area_2d_body_exited(body):
	exit.hide()





func _on_area_2d_2_body_entered(body):
	if body.has_method("body_method"):
		get_tree().change_scene_to_file("res://scenes/menu/menu.tscn")


func _on_q_bentry_body_entered(body):
	if body.has_method("body_method"):
		get_tree().change_scene_to_file("res://scenes/board.tscn")


func _on_travelenrty_body_entered(body):
	if body.has_method("body_method"):
		get_tree().change_scene_to_file("res://scenes/map/map.tscn");


func _on_exitarea_body_entered(body):
	exit.show()


func _on_exitarea_body_exited(body):
	exit.hide()



func _on_tagarea_body_entered(body):
	portal.show()


func _on_tagarea_body_exited(body):
	portal.hide()


func _on_cryptdex_body_entered(body):
	if body.has_method("body_method"):
		get_tree().change_scene_to_file("res://Cards/cryptdex.tscn");
