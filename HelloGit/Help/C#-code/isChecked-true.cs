// Установка свойства в true
Entries.Where(q => !q.isChecked && q.isCanBeChecked).ToList().ForEach(q => q.isChecked = true);


