import {
  MaterialReactTable,
  type MRT_ColumnDef,
  type MRT_Row,
  type MRT_Cell,
} from 'material-react-table';
import { Box, IconButton, Tooltip } from '@mui/material';
import { Delete, Edit } from '@mui/icons-material';

interface DataTableProps<T extends Record<string, any>> {
  data: T[];
  columns: MRT_ColumnDef<T>[];
  onEdit?: (row: MRT_Row<T>) => void;
  onDelete?: (row: MRT_Row<T>) => void;
  isLoading?: boolean;
}

export const DataTable = <T extends Record<string, any>>({
  data,
  columns,
  onEdit,
  onDelete,
  isLoading = false,
}: DataTableProps<T>) => {
  return (
    <MaterialReactTable
      columns={columns}
      data={data}
      enableRowActions
      enableRowSelection
      enableColumnFilters
      enableSorting
      enableGlobalFilter
      state={{ isLoading }}
      renderRowActions={({ row }) => (
        <Box sx={{ display: 'flex', gap: '1rem' }}>
          {onEdit && (
            <Tooltip title="Редактировать">
              <IconButton onClick={() => onEdit(row)}>
                <Edit />
              </IconButton>
            </Tooltip>
          )}
          {onDelete && (
            <Tooltip title="Удалить">
              <IconButton color="error" onClick={() => onDelete(row)}>
                <Delete />
              </IconButton>
            </Tooltip>
          )}
        </Box>
      )}
      muiTableProps={{
        sx: {
          tableLayout: 'fixed',
        },
      }}
      muiTableHeadCellProps={{
        sx: {
          fontWeight: 'bold',
          fontSize: '14px',
        },
      }}
      muiTableBodyCellProps={{
        sx: {
          fontSize: '14px',
        },
      }}
    />
  );
}; 