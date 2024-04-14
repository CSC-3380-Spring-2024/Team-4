extends CharacterBody2D

@onready var animations = $AnimationPlayer

func _physics_process(delta):
	var speed: float = 5
	if Input.is_key_pressed(KEY_W):
		animations.play("Walk-up")
		position.y -= speed
	elif Input.is_key_pressed(KEY_A):
		animations.play("Walk-left")
		position.x -= speed
	elif Input.is_key_pressed(KEY_S):
		animations.play("Walk-down")
		position.y += speed
	elif Input.is_key_pressed(KEY_D):
		animations.play("Walk-right")
		position.x += speed
	else:
		animations.play("Idle")
	
	move_and_slide()

