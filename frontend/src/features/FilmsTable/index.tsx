import { useEffect, useState } from 'react';
import { DataTable } from '@/shared/ui/DataTable';
import { MRT_ColumnDef, MRT_Row } from 'material-react-table';
import { Film } from '@/shared/types/film';
import { axiosSettings } from '@/shared/api/axiosSettings';

const DEFAULT_IMAGE = 'http://localhost:5210/images/category/action.png';

export const FilmsTable = () => {
    const [films, setFilms] = useState<Film[]>([]);
    const [isLoading, setIsLoading] = useState(true);

    const fetchFilms = async () => {
        try {
            const response = await axiosSettings.get<Film[]>('/Film/GetAllFilms');
            setFilms(response.data);
        } catch (error) {
            console.error('Error fetching films:', error);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        fetchFilms();
    }, []);

    const handleEdit = async (row: MRT_Row<Film>) => {
        try {
            await axiosSettings.put(`/Film/UpdateFilm/${row.original.id}`, row.original);
            await fetchFilms();
        } catch (error) {
            console.error('Error updating film:', error);
        }
    };

    const handleDelete = async (row: MRT_Row<Film>) => {
        try {
            await axiosSettings.delete(`/Film/DeleteFilm/${row.original.id}`);
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
            Cell: ({ cell }) => new Date(cell.getValue<string>()).toLocaleDateString(),
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
        <DataTable
            columns={columns}
            data={films}
            onEdit={handleEdit}
            onDelete={handleDelete}
            isLoading={isLoading}
        />
    );
};
