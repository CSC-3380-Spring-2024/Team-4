extends Node

<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< Updated upstream
static var CardList = ["Barghest","GoatMan","Megalodon","Mapinguari","LeechMan"]
=======
static var CardList = ["Barghest","Bigfoot","Megalodon","Mapinguari","Chupacabra"]
>>>>>>> Stashed changes
=======
static var CardList = ["Barghest","Bigfoot","Megalodon","Mapinguari","Chupacabra"]
>>>>>>> Stashed changes
=======
static var CardList = ["Barghest","GoatMan","Megalodon","Mapinguari","LeechMan"]
>>>>>>> bce5a00c6e72334443f88009f9fd0e954d28dd38

func change(rep: String, with: String):
    var index = CardList.search(rep)
    CardList[index] = with
