using System;
using Liquip.WASM;
using Liquip.WASM.Interfaces;

namespace Liquip.Process.WASM;

public class WASI: IWasi
{

    public WASI(WASMProcess process)
    {
    }

    public WasmValue[] args_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] args_sizes_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] environ_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] environ_sizes_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] clock_res_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] clock_time_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_advise(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_allocate(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_close(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_datasync(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_fdstat_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_fdstat_set_flags(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_fdstat_set_rights(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_filestat_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_filestat_set_size(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_filestat_set_times(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_pread(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_prestat_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_prestat_dir_name(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_pwrite(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_read(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_readdir(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_renumber(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_seek(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_sync(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_tell(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] fd_write(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_create_directory(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_filestat_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_filestat_set_times(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_link(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_open(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_readlink(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_remove_directory(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_rename(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_symlink(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] path_unlink_file(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] poll_oneoff(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] proc_exit(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] proc_raise(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] sched_yield(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] random_get(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] sock_accept(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] sock_recv(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] sock_send(WasmValue[] args)
    {
        throw new NotImplementedException();
    }

    public WasmValue[] sock_shutdown(WasmValue[] args)
    {
        throw new NotImplementedException();
    }
}
