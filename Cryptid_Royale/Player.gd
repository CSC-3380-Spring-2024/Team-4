extends CharacterBody2D

var speed: float = 1
var flag: bool  = false
var flagb: bool = true
@onready var animations = $AnimationPlayer

func _physics_process(delta):
	if Input.is_key_pressed(KEY_W):
		animations.play("Walk-up")
		position.y -= speed
		flag = true
	elif Input.is_key_pressed(KEY_A):
		animations.play("Walk-left")
		position.x -= speed
		flag = false
	elif Input.is_key_pressed(KEY_S):
		animations.play("Walk-down")
		position.y += speed
		flag = false
	elif Input.is_key_pressed(KEY_D):
		animations.play("Walk-right")
		position.x += speed
		flag = false
	else:
			animations.play("Idle")
	move_and_slide()
	
func body_method():
	pass

