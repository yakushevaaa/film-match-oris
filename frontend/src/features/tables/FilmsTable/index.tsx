import { MRT_ColumnDef, MRT_Row } from 'material-react-table';
import { Film } from '@/shared/types/film';
import { DataTable } from '@/shared/ui/DataTable';
import { axiosSettings } from '@/shared/api/axiosSettings';
import { AddFilmModal } from '@/features/AddFilmModal';
import { useState } from 'react';

const DEFAULT_IMAGE = 'http://localhost:5210/images/category/action.png';

interface FilmsTableProps {
    films: Film[];
    isLoading: boolean;
    fetchFilms: () => Promise<void>;
}

export const FilmsTable = ({ films, isLoading, fetchFilms }: FilmsTableProps) => {
    const [editModalOpen, setEditModalOpen] = useState(false);
    const [editFilm, setEditFilm] = useState<Film | null>(null);

    const handleEdit = async (row: MRT_Row<Film>) => {
        setEditFilm(row.original);
        setEditModalOpen(true);
    };

    const handleEditSubmit = async (formData: FormData) => {
        try {
            await axiosSettings.put(`/Film/${editFilm?.id}`, formData, {
                headers: { 'Content-Type': 'multipart/form-data' },
            });
            setEditModalOpen(false);
            setEditFilm(null);
            await fetchFilms();
        } catch (error) {
            console.error('Error updating film:', error);
        }
    };

    const handleDelete = async (row: MRT_Row<Film>) => {
        try {
            await axiosSettings.delete(`/Film/${row.original.id}`);
            await fetchFilms();
        } catch (error) {
            console.error('Error deleting film:', error);
        }
    };

    const columns: MRT_ColumnDef<Film>[] = [
        {
            accessorKey: 'title',
            header: 'Title',
        },
        {
            accessorKey: 'releaseDate',
            header: 'Release Date',
            Cell: ({ cell }) => {
                const date = new Date(cell.getValue<string>());
                return isNaN(date.getTime()) ? '' : date.getFullYear();
            },
        },
        {
            accessorKey: 'imageUrl',
            header: 'Image',
            Cell: ({ cell }) => (
                <img 
                    src={cell.getValue<string>() || DEFAULT_IMAGE} 
                    alt="Film poster" 
                    style={{ width: '100px', height: '150px', objectFit: 'cover' }} 
                />
            ),
        },
        {
            accessorKey: 'shortDescription',
            header: 'Short Description',
        },
        {
            accessorKey: 'category.name',
            header: 'Category',
        },
    ];

    return (
        <>
            <DataTable
                columns={columns}
                data={films}
                onEdit={handleEdit}
                onDelete={handleDelete}
                isLoading={isLoading}
            />
            {editModalOpen && editFilm && (
                <AddFilmModal
                    isOpen={editModalOpen}
                    onClose={() => { setEditModalOpen(false); setEditFilm(null); }}
                    onSuccess={() => {}}
                    editMode={true}
                    initialData={{
                        ...editFilm,
                        categoryId: editFilm.category?.id?.toString() || editFilm.categoryId?.toString() || '',
                        releaseDate: editFilm.releaseDate ? editFilm.releaseDate.split('T')[0] : '',
                        imageUrl: editFilm.imageUrl || '',
                    }}
                    onSubmitCustom={handleEditSubmit}
                />
            )}
        </>
    );
};
