
// Глобальная переменная для хранения текущего уведомления
let currentNotification: HTMLDivElement | null = null;

export function popNotification(message: string, type: 'success' | 'error' = 'success') {
    try {
        const app = document.querySelector('.App');
        if (!app) {
            throw new Error('App container not found');
        }

        // Если уже есть уведомление — удалить
        if (currentNotification) {
            app.removeChild(currentNotification);
            currentNotification = null;
        }

        // Создаём новое уведомление
        const notification = document.createElement('div');
        notification.innerHTML = 
        `
        <div class = "timer h-[10px]"></div>
        <div class = "p-[10px] text-left">${message}</div>
        
        `
        notification.className = `notification ${type} flex flex-col absolute top-[10px] right-[10px] bg-[rgb(255,255,255)] font-Kanit text-[24px] rounded-[10px] h-[200px] w-[400px] overflow-hidden`;

        // Добавляем в DOM
        const timer = notification.querySelector('.timer') as HTMLDivElement;

        if (type === 'success') {
            timer.style.backgroundColor = 'rgb(138, 207, 142)'; // Зеленый цвет для успеха
        }
        else if(type === 'error') {
            timer.style.backgroundColor = 'rgb(255, 100, 100)'; // Красный цвет для ошибки
        }

        // Сохраняем как текущее
        currentNotification = notification;

        app.appendChild(notification);
         

        

        if (timer) {
            timer.style.width = '100%';
            timer.style.transition = '10s linear';

            requestAnimationFrame(() => {
                timer.style.width = '0%';
            })
        }

        // Удаляем через 3 секунды с анимацией
        setTimeout(() => {
            if (app.contains(notification)) {
                app.removeChild(notification);
                currentNotification = null;
            }
        }, 10000);

    } catch (error) {
        console.error('Ошибка при создании уведомления:', error);
    }
}

