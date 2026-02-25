import { useCallback, useEffect, useState } from 'react';
import { habitApi, categoryApi } from './api';
import type { HabitResponseDTO, CategoryDTO } from './types';

function App() {
    const [habits, setHabits] = useState<HabitResponseDTO[]>([]);
    const [categories, setCategories] = useState<CategoryDTO[]>([]);
    const [habitName, setHabitName] = useState('');
    const [catId, setCatId] = useState('');
    const [newCatName, setNewCatName] = useState('');
    const [loading, setLoading] = useState(false);
    const [actionLoading, setActionLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const fetchData = useCallback(async () => {
        setLoading(true);
        setError(null);
        try {
            const [h, c] = await Promise.all([habitApi.getAll(), categoryApi.getAll()]);
            setHabits(h.data);
            setCategories(c.data);
        } catch (e) {
            console.error('API Error:', e);
            setError('Ошибка при загрузке данных');
        } finally {
            setLoading(false);
        }
    }, []);

    useEffect(() => { fetchData(); }, [fetchData]);

    const addHabit = useCallback(async () => {
        if (!habitName || !catId) return;
        setActionLoading(true);
        try {
            await habitApi.create({ name: habitName, categoryId: catId });
            setHabitName('');
            setCatId('');
            await fetchData();
        } catch (e) {
            console.error(e);
            setError('Не удалось добавить привычку');
        } finally {
            setActionLoading(false);
        }
    }, [habitName, catId, fetchData]);

    const addCategory = useCallback(async () => {
        if (!newCatName) return;
        setActionLoading(true);
        try {
            await categoryApi.create(newCatName);
            setNewCatName('');
            await fetchData();
        } catch (e) {
            console.error(e);
            setError('Не удалось добавить категорию');
        } finally {
            setActionLoading(false);
        }
    }, [newCatName, fetchData]);

    const toggleComplete = useCallback(async (id: string) => {
        setActionLoading(true);
        try {
            await habitApi.complete(id);
            await fetchData();
        } catch (e) {
            console.error(e);
            setError('Не удалось отметить привычку');
        } finally { setActionLoading(false); }
    }, [fetchData]);

    const removeHabit = useCallback(async (id: string) => {
        setActionLoading(true);
        try {
            await habitApi.delete(id);
            await fetchData();
        } catch (e) {
            console.error(e);
            setError('Не удалось удалить привычку');
        } finally { setActionLoading(false); }
    }, [fetchData]);

    const completedCount = habits.filter(h => h.isCompleted).length;

    return (
        <div className="app-container">

            {/* ── Header ── */}
            <div className="header">
                <div className="header-icon">🎯</div>
                <div>
                    <h1 className="title">Habit Tracker</h1>
                    <p className="subtitle">
                        {habits.length > 0
                            ? `${completedCount} / ${habits.length} выполнено сегодня`
                            : 'Начни отслеживать привычки'}
                    </p>
                </div>
            </div>

            {/* ── Add Category ── */}
            <p className="section-label">Новая категория</p>
            <div className="card" style={{ marginBottom: 20 }}>
                <div className="row">
                    <input
                        className="input"
                        value={newCatName}
                        onChange={e => setNewCatName(e.target.value)}
                        onKeyDown={e => e.key === 'Enter' && addCategory()}
                        placeholder="Название категории…"
                    />
                    <button className="btn" onClick={addCategory} disabled={actionLoading || !newCatName}>
                        + Добавить
                    </button>
                </div>
            </div>

            {/* ── Add Habit ── */}
            <p className="section-label">Новая привычка</p>
            <div className="card" style={{ marginBottom: 28 }}>
                <div className="row">
                    <input
                        className="input"
                        value={habitName}
                        onChange={e => setHabitName(e.target.value)}
                        onKeyDown={e => e.key === 'Enter' && addHabit()}
                        placeholder="Название привычки…"
                    />
                    <select className="select" value={catId} onChange={e => setCatId(e.target.value)}>
                        <option value="">Категория…</option>
                        {categories.map(c => (
                            <option key={c.id} value={c.id}>{c.name}</option>
                        ))}
                    </select>
                    <button className="btn" onClick={addHabit} disabled={actionLoading || !habitName || !catId}>
                        + Добавить
                    </button>
                </div>
            </div>

            <div className="divider" />

            {/* ── Habit list ── */}
            {loading ? (
                <div className="state-box">
                    <div className="spinner" />
                    <p className="state-desc">Загрузка…</p>
                </div>
            ) : error ? (
                <div className="state-box error">
                    <span className="state-icon">⚠️</span>
                    <p className="state-title">Что-то пошло не так</p>
                    <p className="state-desc">{error}</p>
                    <button className="btn" style={{ marginTop: 8 }} onClick={fetchData}>
                        Повторить
                    </button>
                </div>
            ) : habits.length === 0 ? (
                <div className="state-box">
                    <span className="state-icon">✨</span>
                    <p className="state-title">Привычек пока нет</p>
                    <p className="state-desc">Добавь первую привычку выше, чтобы начать</p>
                </div>
            ) : (
                <div className="habit-list">
                    {habits.map(h => (
                        <div key={h.id} className={`habit-item ${h.isCompleted ? 'completed-item' : ''}`}>
                            <div className="habit-meta">
                                <input
                                    type="checkbox"
                                    checked={h.isCompleted}
                                    onChange={() => toggleComplete(h.id)}
                                    disabled={actionLoading}
                                />
                                <div className="habit-text">
                                    <div className={`habit-name ${h.isCompleted ? 'completed' : ''}`}>
                                        {h.name}
                                    </div>
                                    <div className="habit-category">{h.category}</div>
                                </div>
                            </div>
                            <button
                                className="icon-btn danger"
                                onClick={() => removeHabit(h.id)}
                                disabled={actionLoading}
                            >
                                Удалить
                            </button>
                        </div>
                    ))}
                </div>
            )}

            <p className="footer-note">Habit Tracker © {new Date().getFullYear()}</p>
        </div>
    );
}

export default App;