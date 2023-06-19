<?php
// Перевірка наявності всіх обов'язкових параметрів
if (!isset($_POST['testXml']) || !isset($_POST['directory']) || !isset($_POST['testName'])) {
    http_response_code(400);
    echo "Не надані всі необхідні параметри";
    exit;
}

// Отримання переданих даних
$testXml = $_POST['testXml'];
$directory = $_POST['directory'];
$testName = $_POST['testName'];

// Шлях до каталогу з тестами
$testsPath = __DIR__ . "/";

// Створення каталогу, якщо він ще не існує
if (!file_exists($testsPath . $directory)) {
    mkdir($testsPath . $directory, 0777, true);
}

// Формування шляху до файлу
$filePath = $testsPath . $directory . "/" . $testName . ".xml";

// Запис тестового XML у файл
if (file_put_contents($filePath, $testXml) === false) {
    http_response_code(500);
    echo "Помилка при збереженні файлу";
    exit;
}

echo "Тест успішно збережено";
?>