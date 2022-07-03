# 2048

### Објаснување на проблемот
На табла со големина 4x4 случајно се генерира една почетна позиција. Веројатностите се следните:
* 0.64 за да има два почетни блока со вредност 2
* 0.16 за да има два почетни блока, првиот со вредност 2 и вториот со вредност 4
* 0.16 за да има два почетни блока, првиот со вредност 4 и вториот со вредност 2
* 0.04 за да има два почетни блока со вредност 4
  
Со користење на стрелките сите блокови се поместуваат во една насока наеднаш. Доколку при ова поместување два блока со иста вредност станат соседни, тогаш тие се спојуваат во еден блок со двојна вредност. При секое поместување се додава и нов блок. За да победи играчот, истиот треба да создаде блок со вредност 2048. Доколку нема слободно место за да се додаде блок, тогаш играчот губи. Играчот може да започне нова игра со кликнување на копчето "New game". Повеќе информации во врска со играта може да се најдат на следниот [линк](https://en.wikipedia.org/wiki/2048_(video_game)).

### Податочни структури и функции
Главната класа е **Game.cs** и во неа се чуваат сите променливи поврзани со играта. Во конструкторот е имплементирано објаснувањето за почетната позиција. Функциите **int[] shiftRight(int[] row)** и **void rotateTable()** се помошни функции и се користат за имплементација на движењата на блоковите во функцијата **void handleMove(string direction)**. Останатите функции се помошни функции и се дел од проверки - дали е валидна состојбата, дали има соседни блокови што може да се спојат, дали играта завршила.  
Во **Form1.cs** се имплементирани настаните за копчињата како и цртањето на таблата. Тука во конструкторот се иницијализираат речникот **tileColors** за мапирање од вредноста на блокот во соодветна боја и речникот **validKeys** за мапирање на движењата преку стрелките. Функцијата **bool ProcessCmdKey(ref Message msg, Keys keyData)** се користи за да се провери дали играчот притиснал некоја стрелка и тука се применува речникот **validKeys** за пократок код. Цртањето е имплементирано во функцијата **void Form1_Paint(object sender, PaintEventArgs e)**. Се користи пенкало за линиите помеѓу блоковите. Се зема вредноста на секој блок и се доделува боја соодветна за таа вредност користејќи го речникот **tileColors**. Блоковите се исцртуваат така што се црта правоаголник со иста висина и ширина и на овој начин се добива квадрат. Потоа квадратот се пополнува со таа боја и доколку блокот немал вредност 0 тогаш се запишува вредноста внатре во квадратот. Секој квадрат се црта на соодветната позиција земајќи ги предвид квадратите што се веќе нацртани пред него според вредностите на бројачите **i** и **j**.

### Слики од екран
![alt text](https://i.imgur.com/guNfEQp.png)  
