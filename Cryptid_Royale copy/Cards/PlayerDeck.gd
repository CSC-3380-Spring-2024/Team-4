extends Node

static var CardList = ["Barghest","Bigfoot","Megalodon","Mapinguari","Chupacabra"]

func change(rep: String, with: String):
    var index = CardList.search(rep)
    CardList[index] = with
