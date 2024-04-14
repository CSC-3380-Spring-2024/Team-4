extends Node

static var CardList = ["Barghest","Bigfoot","Megalodon","Megalodon","Megalodon"]

func change(rep: String, with: String):
	var index = CardList.search(rep)
	CardList[index] = with
