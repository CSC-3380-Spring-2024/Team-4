extends TextureButton

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
var Cardname = "Megalodon"
=======
var Cardname = "Bigfoot"
>>>>>>> Stashed changes
=======
var Cardname = "Bigfoot"
>>>>>>> Stashed changes
=======
var Cardname = "Megalodon"
>>>>>>> bce5a00c6e72334443f88009f9fd0e954d28dd38

@onready var CardDatabase = load("res://Cards/CardDatabase.gd")
@onready var CardInfo = CardDatabase.DATA[CardDatabase.get(Cardname)]
@onready var CardImg = str("res://Card_Sprites/",Cardname,".png")
var stats = load("res://cryptdex.tscn")
func _ready():
	$Card.texture = load(CardImg)

	#sets which border to use for card based on terrain of cryptid's habitat
	var border = str(CardInfo[0])
	if border == "Water":
		$WaterBorder.visible = true
		$RockyBorder.visible = false
		$AirBorder.visible = false
	elif border == "Rocky":
		$WaterBorder.visible = false
		$RockyBorder.visible = true
		$AirBorder.visible = false
	else:
		$WaterBorder.visible = false
		$RockyBorder.visible = false
		$AirBorder.visible = true

	#stats stored for stats page for cards
	var Attack = str(CardInfo[1])
	var Speed = str(CardInfo[2])
	var DamageRes = str(CardInfo[3])
	$StatMenu/Window/ImgDescr/cryptidDescr.text = CardInfo[6]
	$StatMenu/Window/ImgDescr/cryptidImg.texture = load(CardImg)
	$StatMenu/Window/StatLevel/Speed.text = Speed
	$StatMenu/Window/StatLevel/Attack.text = Attack
	$StatMenu/Window/StatLevel/Defense.text = DamageRes
	#setting the type of card for letter identification
	var Type = str(CardInfo[5])
	$Bars/Top/Type/CenterContainer/Label.text = Type
	
	#set name of cryptid
	$Bars/Bottom/Name/CenterContainer/Label.text = Cardname
func _pressed():
	print(Cardname + " has been pressed")
	$StatMenu.visible = true
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
	print($StatMenu.get_viewport().size)
=======
	print($StatMenu.get_viewport().size)
>>>>>>> Stashed changes
=======
	print($StatMenu.get_viewport().size)
>>>>>>> Stashed changes
=======
	print($StatMenu.get_viewport().size)
>>>>>>> bce5a00c6e72334443f88009f9fd0e954d28dd38
