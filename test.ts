/* eslint-disable react/no-unused-prop-types */
import { ExpandLess, ExpandMore } from "@mui/icons-material";
import { CircularProgress, styled } from "@mui/material";
import {
  GridToolbar,
  useGridApiRef,
  GridColDef,
  GridRowsProp,
  GridRowSelectionModel,
  GridRowParams,
  GridCallbackDetails,
  MuiEvent,
  GridRowClassNameParams,
  DataGridPremium,
  GridColumnVisibilityModel,
  GridGroupingColDefOverride,
  GridValidRowModel,
  GridFilterModel,
  GridSortModel,
  GridPaginationModel,
  GridEventListener,
  GridCellParams,
  GridTreeNode,
  GridRowGroupingModel,
  GridEditMode,
  GridRowModesModel,
  GridToolbarProps,
  ToolbarPropsOverrides,
} from "@mui/x-data-grid-premium";
import { GridInitialStatePremium } from "@mui/x-data-grid-premium/models/gridStatePremium";
import React from "react";
import { boitDataGridStyles } from "./datagridhelpers/boitDataGrid.styles";

export interface IBoitDataGrid {
  getRowId: (row: any) => number;
  columns: readonly GridColDef<any>[];
  rows: GridRowsProp<any>;
  loading?: boolean;
  style?: any;
  checkboxSelection?: boolean;
  rowSelectionModel?: GridRowSelectionModel;
  sortingMode?: "client" | "server";
  rowHeight?: number;
  filterMode?: "client" | "server";
  pageSizeOptions?: number[];
  disableColumnMenu?: boolean;
  isRowSelectable?: (_params: GridRowParams) => boolean;
  rowCount?: number;
  headerFilterEnabled?: boolean;
  rightClickEnabled?: boolean;
  handleRowContextMenu?: (e: any) => undefined;
  autoHeight?: boolean;
  pagination?: boolean;
  hideFooterRowCount?: boolean;
  hideToolBar?: boolean;
  customToolbar?: () => JSX.Element;
  onRowClick?: (params: GridRowParams, event: MuiEvent, details: GridCallbackDetails) => void;
  initialState?: any;
  getDetailPanelContent?: ((params: GridRowParams<any>) => React.ReactNode) | undefined;
  hideQuickFilter?: boolean;
  dataGridId: string;
  getRowClassName?: ((params: GridRowClassNameParams<any>) => string) | undefined;
  disableAggregation?: boolean;
  getDetailPanelHeight?: ((params: GridRowParams<any>) => number | "auto") | undefined;
  fileName?: string;
  defaultGroupingExpansionDepth?: number;
  columnVisibilityModel?: GridColumnVisibilityModel;
  groupingColDef?: GridGroupingColDefOverride<GridValidRowModel>;
  onColumnVisibilityModelChange?: (model: GridColumnVisibilityModel, details: GridCallbackDetails<any>) => void;
  filterModel?: GridFilterModel | undefined;
  onFilterModelChange?: ((model: GridFilterModel, details: GridCallbackDetails<"filter">) => void) | undefined;
  sortModel?: GridSortModel | undefined;
  onSortModelChange?: ((model: GridSortModel, details: GridCallbackDetails<any>) => void) | undefined;
  paginationModel?: GridPaginationModel | undefined;
  onPaginationModelChange?: ((model: GridPaginationModel, details: GridCallbackDetails<any>) => void) | undefined;
  disableToolbarPrintOption?: boolean;
  onCellClick?: GridEventListener<"cellClick"> | undefined;
  getCellClassName?:
    | ((params: GridCellParams<any, GridValidRowModel, GridValidRowModel, GridTreeNode>) => string)
    | undefined;
  customFooter?: () => JSX.Element;
  rowGroupingModel?: GridRowGroupingModel;
  disableToolbarCSV?: boolean;
  editMode?: GridEditMode | undefined;
  rowModesModel?: GridRowModesModel | undefined;
  onRowModesModelChange?: ((rowModesModel: GridRowModesModel, details: GridCallbackDetails<any>) => void) | undefined;
  onRowEditStop?: GridEventListener<"rowEditStop"> | undefined;
  processRowUpdate?:
    | ((newRow: GridValidRowModel, oldRow: GridValidRowModel) => GridValidRowModel | Promise<GridValidRowModel>)
    | undefined;
  toolBarSlotProps?: Partial<GridToolbarProps & ToolbarPropsOverrides> | undefined;
  noRowsLabel?: string | undefined;
}

const StyledDataGridPremium = styled(DataGridPremium)(({ theme }) => boitDataGridStyles(theme));

export function BoitDataGrid(props: IBoitDataGrid): JSX.Element {
  const {
    getRowId,
    columns,
    rows,
    loading,
    style,
    checkboxSelection,
    rowSelectionModel,
    sortingMode,
    rowHeight,
    filterMode,
    pageSizeOptions,
    disableColumnMenu,
    rowCount,
    headerFilterEnabled,
    rightClickEnabled,
    handleRowContextMenu,
    autoHeight,
    pagination,
    hideFooterRowCount,
    customToolbar,
    onRowClick,
    initialState,
    getDetailPanelContent,
    hideQuickFilter = false,
    dataGridId,
    getRowClassName,
    disableAggregation,
    getDetailPanelHeight,
    fileName,
    defaultGroupingExpansionDepth,
    columnVisibilityModel,
    groupingColDef,
    onColumnVisibilityModelChange,
    filterModel,
    onFilterModelChange,
    sortModel,
    onSortModelChange,
    paginationModel,
    onPaginationModelChange,
    disableToolbarPrintOption = false,
    onCellClick,
    getCellClassName,
    customFooter,
    rowGroupingModel,
    disableToolbarCSV = false,
    editMode,
    rowModesModel,
    onRowModesModelChange,
    onRowEditStop,
    processRowUpdate,
    toolBarSlotProps = {},
    noRowsLabel = "No data to display",
  } = props;
  const apiRef = useGridApiRef();
  const [initialGridState, setInitialState] = React.useState<GridInitialStatePremium>();

  const csvExcelOptions = React.useMemo(
    () => ({
      fileName,
      disableToolbarButton: disableToolbarCSV,
    }),
    [disableToolbarCSV, fileName],
  );
  const memorizedColumns = React.useMemo(() => columns, [columns]);

  const saveSnapshot = React.useCallback(() => {
    if (apiRef?.current?.exportState && localStorage) {
      const id = `BMP2_${dataGridId}`;
      const currentState = apiRef.current.exportState();
      // do not store filter
      const toStore = { ...currentState, filter: {} };
      localStorage.setItem(id, JSON.stringify(toStore));
    }
  }, [apiRef, dataGridId]);

  React.useLayoutEffect(() => {
    const id = `BMP2_${dataGridId}`;

    const stateFromLocalStorage = localStorage?.getItem(id);
    setInitialState(stateFromLocalStorage ? JSON.parse(stateFromLocalStorage) : {});

    // handle refresh and navigating away/refreshing
    window.addEventListener("beforeunload", saveSnapshot);

    return () => {
      //  remove the event-listener
      window.removeEventListener("beforeunload", saveSnapshot);
      saveSnapshot();
    };
  }, [dataGridId, saveSnapshot]);

  const memoizedPageSizeOptions = React.useMemo(() => pageSizeOptions || [10, 20, 30, 50, 100], [pageSizeOptions]);
  if (!initialGridState) {
    return <CircularProgress />;
  }

  return (
    <StyledDataGridPremium
      apiRef={apiRef}
      autoHeight={autoHeight || false}
      checkboxSelection={checkboxSelection || false}
      columns={memorizedColumns}
      disableColumnMenu={disableColumnMenu || false}
      filterMode={filterMode || "client"}
      getRowId={getRowId}
      getDetailPanelHeight={getDetailPanelHeight}
      getDetailPanelContent={getDetailPanelContent}
      hideFooterRowCount={hideFooterRowCount || false}
      disableAggregation={disableAggregation || true}
      filterModel={filterModel}
      onFilterModelChange={onFilterModelChange}
      onSortModelChange={onSortModelChange}
      sortModel={sortModel}
      getCellClassName={getCellClassName}
      initialState={{
        ...(initialState ?? {}),
        // Use an empty object if initialState is undefined
        ...initialGridState,
      }}
      loading={loading || false}
      onRowClick={onRowClick}
      pageSizeOptions={memoizedPageSizeOptions}
      pagination={pagination == null ? true : pagination}
      paginationMode={sortingMode || "client"}
      paginationModel={paginationModel}
      onCellClick={onCellClick}
      onPaginationModelChange={onPaginationModelChange}
      // Density Selector not needed for BOIT products
      disableDensitySelector
      rowCount={rowCount}
      rowHeight={rowHeight || 52}
      rows={rows}
      rowSelectionModel={rowSelectionModel}
      sortingMode={sortingMode || "client"}
      style={style}
      density="compact"
      unstable_headerFilters={headerFilterEnabled === undefined ? true : headerFilterEnabled}
      slots={{
        toolbar: customToolbar || GridToolbar,
        detailPanelExpandIcon: ExpandMore,
        detailPanelCollapseIcon: ExpandLess,
        footer: customFooter || undefined,
      }}
      slotProps={{
        cell: {
          style: {
            fontSize: "smaller",
          },
        },
        row: {
          onContextMenu:
            rightClickEnabled && handleRowContextMenu ? (e: any) => handleRowContextMenu(e) : () => undefined,
          style: {
            cursor: `${rightClickEnabled ? "context-menu" : "pointer"}`,
          },
        },
        toolbar: {
          csvOptions: csvExcelOptions,
          showQuickFilter: !hideQuickFilter,
          printOptions: { disableToolbarButton: disableToolbarPrintOption },

          excelOptions: csvExcelOptions,
          ...toolBarSlotProps,
        },
      }}
      getRowClassName={getRowClassName}
      defaultGroupingExpansionDepth={defaultGroupingExpansionDepth}
      columnVisibilityModel={columnVisibilityModel}
      groupingColDef={groupingColDef}
      onColumnVisibilityModelChange={onColumnVisibilityModelChange}
      rowGroupingModel={rowGroupingModel}
      editMode={editMode}
      rowModesModel={rowModesModel}
      onRowModesModelChange={onRowModesModelChange}
      onRowEditStop={onRowEditStop}
      processRowUpdate={processRowUpdate}
      localeText={{
        noRowsLabel,
      }}
    />
  );
}
