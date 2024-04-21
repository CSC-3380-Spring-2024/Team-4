class_name Menu

extends Control

@onready var title = $"Title-Screne";
@onready var menu = $"Main-Menu";
@onready var options = $Options;
@onready var audio = $Audio;
@onready var home = $Home;
@onready var map = $Map; 



func _ready():
	title.show();
	menu.hide();
	home.hide();
	options.hide();
	audio.hide();
	home.hide();
	

	

#show one node and hide the other
func show_and_hide(first, second):
	first.show();
	second.hide();

# Start button to transition from the title screen to the Main menu
func _on_play_pressed():
	show_and_hide(menu, title);

# Button to see Play options
func _on_start_pressed():
	get_tree().change_scene_to_file("res://scenes/home/home.tscn");

#Button to go to the Options
func _on_options_pressed():
	show_and_hide(options, menu);

# Button to close game
func _on_exit_pressed():
	get_tree().quit();

# Button to go to audio settings
func _on_audio_pressed():
	show_and_hide(audio, options);

# Button to go back to Main menu form Settings
func _on_back_settings_pressed():
	show_and_hide(menu, options);

# Function to adjust sound sliders
func volume(bus_index, value):
	AudioServer.set_bus_volume_db(bus_index, value)

# Master volume settings slider
func _on_master_value_changed(value):
	volume(0, -value);

# Music settings slider
func _on_music_value_changed(value):
	volume(1, -value);

# Sound FX settings slider
func _on_sound_fx_value_changed(value):
	volume(2, -value);


# button to go to audio settings
func _on_back_audio_pressed():
	show_and_hide(options, audio);



