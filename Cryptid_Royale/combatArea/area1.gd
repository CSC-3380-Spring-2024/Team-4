extends Area3D

var tick: int = 0;
# Called when the node enters the scene tree for the first time.
func _ready():
	if(body_entered):
		tick =+ 1;
		print(tick)
		
	if(body_exited):
		tick =- 1;
		print(tick) # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):

