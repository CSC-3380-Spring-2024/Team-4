extends CharacterBody3D	


@onready var navigationAgent := $NavigationAgent3D
var speed = 10
# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if(navigationAgent.is_navigation_finished()):
		return
	
	onInputEvent()#where movetopos() was called 
	pass
	
func moveToPos(delta, speed):
	var targetPos = navigationAgent.get_next_path_position()
	var direction = global_position.direction_to(targetPos)
	
	velocity = direction * speed
	move_and_slide()
	for index in get_slide_collision_count():
		var collision := get_slide_collision(index)		
		var body := collision.get_collider()
		print("Collided with: ", body.name)
	
func _input(event):
	if Input.is_action_just_pressed("LeftMouse"):
		var camera = get_tree().get_nodes_in_group("Camera")[0]
		var mousePos = get_viewport().get_mouse_position()
		var rayLength = 100
		var from = camera.project_ray_origin(mousePos)
		var to = from + camera.project_ray_normal(mousePos) * rayLength
		var space = get_world_3d().direct_space_state
		var rayQuery = PhysicsRayQueryParameters3D.new()
		rayQuery.from = from
		rayQuery.to = to
		var result = space.intersect_ray(rayQuery)
		print(result)
		
		navigationAgent.target_position = result.position


func onEnter(area):
	print("success")

func onInputEvent():
	if(event.is_action_pressed("LeftMouse")):
		moveToPos(delta, speed)

