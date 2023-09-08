using System;
using Liquip.WASM;
using Liquip.WASM.Interfaces;

namespace Liquip.Process.WASM;

public class WASI: IWasi
{

    public WASI(WASMProcess process)
    {
    }

    public Value[] args_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] args_sizes_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] environ_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] environ_sizes_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] clock_res_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] clock_time_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_advise(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_allocate(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_close(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_datasync(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_fdstat_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_fdstat_set_flags(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_fdstat_set_rights(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_filestat_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_filestat_set_size(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_filestat_set_times(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_pread(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_prestat_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_prestat_dir_name(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_pwrite(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_read(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_readdir(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_renumber(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_seek(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_sync(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_tell(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] fd_write(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_create_directory(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_filestat_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_filestat_set_times(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_link(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_open(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_readlink(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_remove_directory(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_rename(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_symlink(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] path_unlink_file(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] poll_oneoff(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] proc_exit(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] proc_raise(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] sched_yield(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] random_get(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] sock_accept(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] sock_recv(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] sock_send(Value[] args)
    {
        throw new NotImplementedException();
    }

    public Value[] sock_shutdown(Value[] args)
    {
        throw new NotImplementedException();
    }
}
