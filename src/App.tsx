import { useCallback, useEffect, useState } from 'react';
import { habitApi, categoryApi } from './api';
import type {HabitResponseDTO, CategoryDTO} from './types';

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

    return (
        <div className="app-container">
            <div className="header">
                <h1 className="title">Habit Tracker</h1>
            </div>

            <div className="card" style={{ marginBottom: 12 }}>
                <div className="row">
                    <input className="input" value={newCatName} onChange={e => setNewCatName(e.target.value)} placeholder="New Category" />
                    <button className="btn" onClick={addCategory} disabled={actionLoading || !newCatName}>Add Category</button>
                </div>
            </div>

            <div className="card" style={{ marginBottom: 12 }}>
                <div className="row">
                    <input className="input" value={habitName} onChange={e => setHabitName(e.target.value)} placeholder="Habit Name" />
                    <select className="select" value={catId} onChange={e => setCatId(e.target.value)}>
                        <option value="">Category...</option>
                        {categories.map(c => <option key={c.id} value={c.id}>{c.name}</option>)}
                    </select>
                    <button className="btn" onClick={addHabit} disabled={actionLoading || !habitName || !catId}>Add Habit</button>
                </div>
            </div>

            {loading ? (
                <div className="center card small-muted">Loading...</div>
            ) : error ? (
                <div className="card small-muted">{error}</div>
            ) : (
                <div className="habit-list">
                    {habits.map(h => (
                        <div key={h.id} className="card habit-item">
                            <div className="habit-meta">
                                <input type="checkbox" checked={h.isCompleted} onChange={() => toggleComplete(h.id)} disabled={actionLoading} />
                                <div>
                                    <div className={`habit-name ${h.isCompleted ? 'completed' : ''}`}>{h.name}</div>
                                    <div className="small-muted">{h.category}</div>
                                </div>
                            </div>
                            <div>
                                <button className="icon-btn danger" onClick={() => removeHabit(h.id)} disabled={actionLoading}>Delete</button>
                            </div>
                        </div>
                    ))}
                </div>
            )}

            <div className="footer-note small-muted">Tip: use categories to group habits.</div>
        </div>
    );
}

export default App;