   M S B A M L   `   `   `  ����C  ?SnakeWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SnakeWPFSnakeWPF  5   	       SnakeWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SnakeWPF     5   	   -   5          �Name$  window��5   	      Program_Title � Title# �   5   	   $	��480��5	   	   $	��480��6   $	��480��5
   	   $	��480��6   $y�Visible=�5   	    � SizeToContent$ WidthAndHeight=�5   	   .��5      � .+�5   
   � -   5         �Name$ GameGrid��5      $y�Hidden=�6!   }�. .+�5      o� $��Center=�6   $	��Top=�65   ���	#FF10722F$��18��6d   .@�5      m�   	Game_Time#���  6   $��Center=�6@   
�������6b   m� 5      -   5         m�Name$ 	TimeValue��5      $��00:00:00��6.   $��Center=�6@   
�������6b   �� 5      m� 5        
Game_Score#���  6   $��Center=�6A   
�������6c   m� 5      -   5        $ 
ScoreValue��5      $��0��6/   $��Center=�6:   
�������6\   5      �� 5      -   5         ��Name$ 	GameField��5   ;   }�. $��0,18,0,0q�6)   $��Stretch=�5      $��Stretch=�61   ���	#FF2399335      �� 5      $y�	Collapsed=�6    �� Orientation$ Vertical=�60   $��3,24,3,3q�6G   $��Right=�6Y   $	��Top=�6u   ���	#BFFFFFFF.+�5      �� $��Debug:��6   �� 5      $��5,0,15,0q�6   ]� 5       .+�5!      �� $
��Left=�6   $��X:��62   �� 5"      -   5         ��Name$ debug_snakeX��5"   3   $��Right=�6   63   5#      ]� 5$      .+�5%      �� $
��Left=�6   $��Y:��62   �� 5&      -   5        $ debug_snakeY��5&   3   $��Right=�6   63   5'      ]� 5(      .+�5)      �� $
��Left=�6   $��Points num:��62   �� 5*      -   5        $ debug_snakePointsNum��5*   3   $��Right=�6   63   5+      ]� 5,      .+�5-      �� $
��Left=�6   $��Last button:��62   �� 5.      -	   5        $ debug_lastButton��5.   3   $��Right=�6   63   5/      50      51      � 53   
   -
   5        $ MenuGrid��53      $y�Visible=�6!   ���	#FF38788D.+�54      �� -   5         ��Name$ languageList��54      	 �� 
IsReadOnly	 . 
 �� 
IsEditable
 .   Language �� Text# �  6Q   $��30��55      $��70��6   $
��Auto��6-   $��Center=�56      $��Center=�67   $	��Top=�57      $��Right=�6+   $��5q�58      �� 59      -   5        $ 
MenuStatus��59        Welcome#���  6(   $��Center=�6L   $��Center=�6i   $��Stretch=�6�   $��
0,0,0, 150q�6�   
������� J� RenderTransformOrigin$ 0.5,0.5&�6�   ��6�   �� *Width+ �� ElementName$ window��z�5:      V� 5;      .~�5<      ��  �� ScaleX$ 2Q�6)    �� ScaleY$ 2Q�64   �� 5=      �� 5>      T� 5?      5@      5B      �� 5C      $��0,40,0,0q�6   $��Center=�6+   $��Center=�6F   $	��400��6c   .+�5D      �� -   5        $��30��5D        	Menu_Play#���  6%   
���������6c   Z� ��6�   Z� �� 5H      -   5        $��30��5H        	Menu_Exit#���  6%   
���������6c   Z� ��6�   Z� 5I      5J      5K      5L      