extends CharacterBody2D

<<<<<<< Updated upstream
<<<<<<< Updated upstream
var speed: float = 2.5
=======
var speed: float = 1
>>>>>>> Stashed changes
=======
var speed: float = 1
>>>>>>> Stashed changes
var flag: bool  = false
var flagb: bool = true
@onready var animations = $AnimationPlayer

func _physics_process(delta):
	if Input.is_key_pressed(KEY_W):
		animations.play("Walk-up")
<<<<<<< Updated upstream
<<<<<<< Updated upstream
		position.y -= speed * 0.5
		flag = true
	elif Input.is_key_pressed(KEY_A):
		animations.play("Walk-left")
		position.x -= speed 
		flag = false
	elif Input.is_key_pressed(KEY_S):
		animations.play("Walk-down")
		position.y += speed * 0.5
=======
=======
>>>>>>> Stashed changes
		position.y -= speed
		flag = true
	elif Input.is_key_pressed(KEY_A):
		animations.play("Walk-left")
		position.x -= speed
		flag = false
	elif Input.is_key_pressed(KEY_S):
		animations.play("Walk-down")
		position.y += speed
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
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

