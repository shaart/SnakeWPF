   M S B A M L   `   `   `  ���� C  ?SnakeWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SnakeWPFSnakeWPF      SnakeWPF.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:SnakeWPF     -     �Name$  window��   Program_Title � Title# �   $	��480��$	��480��$	��480��$	��480��$y�Visible=� � SizeToContent$ WidthAndHeight=�.��� .+�� -    �Name$ GameGrid��$y�Hidden=�}�. .+�o� $��Center=�$	��Top=����	#FF10722F$��18��.@�m�   	Game_Time#���  $��Center=�
�������m� -    m�Name$ 	TimeValue��$��00:00:00��$��Center=�
��������� m�   
Game_Score#���  $��Center=�
�������m� -   $ 
ScoreValue��$��0��$��Center=�
��������� -    ��Name$ 	GameField��}�. $��0,18,0,0q�$��Stretch=�$��Stretch=����	#FF239933�� $y�	Collapsed=� �� Orientation$ Vertical=�$��3,24,3,3q�$��Right=�$	��Top=����	#BFFFFFFF.+��� $��Debug:���� $��5,0,15,0q�]� .+��� $
��Left=�$��X:���� -    ��Name$ debug_snakeX��$��Right=�]� .+��� $
��Left=�$��Y:���� -   $ debug_snakeY��$��Right=�]� .+��� $
��Left=�$��Points num:���� -   $ debug_snakePointsNum��$��Right=�]� .+��� $
��Left=�$��Last button:���� -	   $ debug_lastButton��$��Right=�� -
   $ MenuGrid��$y�Visible=����	#FF38788D.+��� -    ��Name$ languageList��	 �� 
IsReadOnly	 . 
 �� 
IsEditable
 .   Language �� Text# �  $��30��$��70��$
��Auto��$��Center=�$��Center=�$	��Top=�$��Right=�$��5q��� -   $ 
MenuStatus��  Welcome#���  $��Center=�$��Center=�$��Stretch=�$��
0,0,0, 150q�
������� J� RenderTransformOrigin$ 0.5,0.5&����� *Width+ �� ElementName$ window��z�V� .~���  �� ScaleX$ 2Q� �� ScaleY$ 2Q��� �� T� �� $��0,40,0,0q�$��Center=�$��Center=�$	��400��.+��� -   $��30��  	Menu_Play#���  
���������Z� ��Z� �� -   $��30��  	Menu_Exit#���  
���������Z� ��Z� 