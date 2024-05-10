extends Node

static var CardList = ["Barghest","GoatMan","Megalodon","Mapinguari","LeechMan"]

func change(rep: String, with: String):
    var index = CardList.search(rep)
    CardList[index] = with
