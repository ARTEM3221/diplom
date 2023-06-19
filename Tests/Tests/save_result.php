<?php
    // Читання вхідних даних
    $name = $_POST["name"];
    $testTheme = $_POST["testTheme"];
    $correctAnswers = $_POST["correctAnswers"];
    $averageScore = $_POST["averageScore"];

    // Створення файлу з результатами
    $filename = "results_" . time() . ".txt";
    $file = fopen($filename, "w") or die("Unable to open file!");

    // Запис результатів у файл
    $content = "Name: {$name}\n";
    $content .= "Test Theme: {$testTheme}\n";
    $content .= "Correct Answers: {$correctAnswers}\n";
    $content .= "Average Score: {$averageScore}\n";
    fwrite($file, $content);

    // Закриття файлу
    fclose($file);

    echo "Result saved successfully";
?>