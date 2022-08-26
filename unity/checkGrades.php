<?php

    //Acceso a los datos del usuario actualmente en línea
    require_once('../config.php');
    global $USER;

    //Obteniendo datos del fichero database.txt
    $handle = fopen("database.txt", "r");
    if ($handle) {
        $i = 0;
        while (($line = fgets($handle)) !== false) {
            $linesplit = explode('=', $line);
            switch($i) {
                case 0: $hostname = substr($linesplit[1], 0, -2); break;
                case 1: $username = substr($linesplit[1], 0, -2); break;
                case 2: $password = substr($linesplit[1], 0, -2); break;
                case 3: $database = substr($linesplit[1], 0, -2); break;
            }
            $i++;
        }

        fclose($handle);
    }

    //Estableciendo conexión con la base de datos
    $dbConnection = mysqli_connect($hostname, $username, $password, $database) or die ("Conexión con la base de datos fallida");

    //En primer lugar, se crea la tabla game_checkedgrades si esta no existe
    $query = "CREATE TABLE IF NOT EXISTS game_checkedgrades (
        ID int(11) AUTO_INCREMENT,
        USERID bigint(10),
        GRADEID bigint(10),
        PRIMARY KEY (ID),
        FOREIGN KEY (USERID) REFERENCES mdl_user(ID),
        FOREIGN KEY (GRADEID) REFERENCES mdl_grade_grades(ID)
    )";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //A continuación se obtienen todas las calificaciones del usuario actual que no han sido ya
    //recopiladas en la tabla game_checkedgrades
    $query = "SELECT g.id, g.userid, g.rawgrade, g.rawgrademax FROM mdl_grade_grades g WHERE g.userid = " . $USER->id . 
        " AND NOT EXISTS (SELECT 1 FROM game_checkedgrades gg WHERE g.id = gg.GRADEID)";

    $result = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

    //Si el usuario cuenta con calificaciones no registradas, se registran y se pasan al juego
    //para su posterior conversión a monedas.
    
    if(mysqli_num_rows($result) > 0) {
        $finalValue = 0;

        for($i=0; $i<mysqli_num_rows($result); $i++) {
            $grade = mysqli_fetch_array($result);

            //Se guarda la calificación en la base de datos
            $query = "INSERT INTO game_checkedgrades VALUES (null, " .
                $USER->id . ", " . $grade['id'] . ");";

            $resultAux = mysqli_query($dbConnection, $query) or die (mysqli_error($dbConnection));

            //Se calcula el total de monedas a entregar al jugador, y se pasa el valor.
            $finalValue += (floatval($grade['rawgrade']) * 100.0 / floatval($grade['rawgrademax'])); 
        }
        echo(round($finalValue, 0));
    }

    else echo("null");

?>