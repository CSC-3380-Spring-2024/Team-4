extends Node2D


var CardBase = load("res://Cards/CardBase.tscn")
var PlayerDeck = load("res://Cards/PlayerDeck.gd")
var Database = load("res://Cards/CardDatabase.gd")

func _ready():
	for n in 5:
		var Deck = PlayerDeck.CardList
		var new_Card = CardBase.instantiate()
		new_Card.Cardname = Deck[n]
		if Deck[n] == " ":
			continue
		if n == 0:
			$"Screen/Current In Deck/Card1".add_child(new_Card)
		elif n == 1:
			$"Screen/Current In Deck/Card2".add_child(new_Card)
		elif n == 2:
			$"Screen/Current In Deck/Card3".add_child(new_Card)
		elif n == 3:
			$"Screen/Current In Deck/Card4".add_child(new_Card)
		elif n == 4:
			$"Screen/Current In Deck/Card5".add_child(new_Card)
	var num = Database.DATA.size()
	var keys = Database.DATA.keys()
	var row = 1
	var column = 1
	for i in 8:
		var new_card = CardBase.instantiate()
		new_card.Cardname = Database.DATA[keys[i]][4]
		var rowName = "Row" + str(row)
		print(rowName)
		var colName = "Card" + str(column)
		print(colName)
		var path = "Bottom/Cards/Scroll/" + rowName + "/" + colName
		get_node(path).add_child(new_card)
		column+=1
		if column > 5:
			row+=1
			column = 1

	
	print(num)



func _on_button_pressed():
	get_tree().change_scene_to_file("res://scenes/home/home.tscn")
