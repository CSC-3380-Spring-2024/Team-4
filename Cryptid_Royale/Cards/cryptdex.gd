extends Node2D


var CardBase = load("res://Cards/CardBase.tscn")
var PlayerDeck = load("res://Cards/PlayerDeck.gd")
var Database = load("res://Cards/CardDatabase.gd")
var popup;
func _enter_tree():
	for n in 5:
		var Deck = PlayerDeck.CardList
		var new_Card = CardBase.instantiate()
		new_Card.Cardname = Deck[n]
		if Deck[n] == " ":
			continue
		if n == 0:
			$"Screen/Current In Deck/Card1".replace_by(new_Card)
		elif n == 1:
			$"Screen/Current In Deck/Card2".replace_by(new_Card)
		elif n == 2:
			$"Screen/Current In Deck/Card3".replace_by(new_Card)
		elif n == 3:
			$"Screen/Current In Deck/Card4".replace_by(new_Card)
		elif n == 4:
			$"Screen/Current In Deck/Card5".replace_by(new_Card)
	var num = Database.DATA.size()
	var keys = Database.DATA.keys()
	var row = 1
	var column = 1
	for i in num:
		var new_card = CardBase.instantiate()
		new_card.Cardname = Database.DATA[keys[i]][4]
		if PlayerDeck.CardList.has(new_card.Cardname):
			new_card.disabled = true
		var rowName = "Row" + str(row)
		var colName = "Card" + str(column)
		var path = "Bottom/Cards/Scroll/" + rowName + "/" + colName
		get_node(path).replace_by(new_card)
		column+=1
		if column > 5:
			row+=1
			column = 1
		

func _process(delta):
	var num = Database.DATA.size()
	var keys = Database.DATA.keys()
	var row = 1
	var column = 1
	for i in num:
		var new_card = CardBase.instantiate()
		new_card.Cardname = Database.DATA[keys[i]][4]
		var rowName = "Row" + str(row)
		var colName = "Card" + str(column)
		var path = "Bottom/Cards/Scroll/" + rowName + "/" + colName
		"""if get_node(path).button_pressed() == true:
			var card = get_node(path)
			var CardImg = str("res://Card_Sprites/",new_card.Cardname,".png")
			$ThisMenu/Window/ImgDescr/cryptidDescr.text = Database.DATA[keys[i][6]]
			$ThisMenu/Window/ImgDescr/cryptidImg.texture = load(CardImg)
			$ThisMenu.visible = true
        """

		column+=1
		if column > 5:
			row+=1
			column = 1
