<?php
header('Content-Type: text/html; charset=utf-8');

// Читання вхідних даних
$json = file_get_contents('php://input');
$data = json_decode($json, true);

$name = str_replace(' ', '_', $data["name"]); // Заміна пробілів на підкреслення в назві також
$testTheme = $data["testTheme"]; // Оригінальна назва теми без заміни
$correctAnswers = $data["correctAnswers"];
$averageScore = $data["averageScore"];

// Перетворення назви теми на латинські символи
$transliteratedTheme = transliterator_transliterate('Any-Latin; Latin-ASCII;', $testTheme);
$dirTheme = preg_replace('/[^A-Za-z0-9 _\-]/', '', $transliteratedTheme);

// Переконуємся, що назва каталогу відповідає вимогам безпеки
$baseDir = __DIR__;
$dir = $baseDir . '/' . str_replace(' ', '_', $dirTheme);

// Створення каталогу, якщо він ще не існує
if (!file_exists($dir)) {
    mkdir($dir, 0777, true);
}

// Створення файлу з результатами
$filename = $dir . "/" . $name . "_results.txt";
$file = fopen($filename, "w") or die("Unable to open file!");

// Запис результатів у файл
$content = "Ім'я: {$name}\n";
$content .= "Тема тесту: {$testTheme}\n";
$content .= "Правильні відповіді: {$correctAnswers}\n";
$content .= "Середній бал: {$averageScore}\n";
fwrite($file, $content);

// Закриття файлу
fclose($file);

echo "Результат успішно збережено";
?>