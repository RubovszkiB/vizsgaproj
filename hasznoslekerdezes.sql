-- Összes edző lekérdezése
SELECT * FROM trainers;

-- Összes edzésterv lekérdezése
SELECT * FROM training_plans;

-- Edzők és az általuk készített edzéstervek
SELECT 
    t.name AS trainer_name,
    t.specialty,
    tp.title AS training_plan,
    tp.category,
    tp.price
FROM trainers t
JOIN training_plans tp ON t.id = tp.trainer_id
ORDER BY t.name;

-- Kategóriánkénti edzéstervek száma
SELECT 
    category,
    COUNT(*) as plan_count,
    AVG(price) as avg_price
FROM training_plans
GROUP BY category;

-- Legdrágább edzéstervek
SELECT title, category, price
FROM training_plans
ORDER BY price DESC
LIMIT 5;
