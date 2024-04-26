extends TextureButton

func _pressed():
    var curr = get_parent().get_parent().get_parent()
    curr.visible = !curr.visible
