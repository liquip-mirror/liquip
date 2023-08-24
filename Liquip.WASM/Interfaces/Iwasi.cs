namespace Liquip.WASM.Interfaces;

/// <summary>
/// WebAssembly System Interface
/// </summary>
public interface IWasi
{

    /// <summary>
    /// args_get(argv: Pointer<Pointer<u8>>, argv_buf: Pointer<u8>) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] args_get(Value[] args);

    /// <summary>
    /// args_sizes_get() -> Result<(size, size), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] args_sizes_get(Value[] args);

    /// <summary>
    /// environ_get(environ: Pointer<Pointer<u8>>, environ_buf: Pointer<u8>) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] environ_get(Value[] args);

    /// <summary>
    /// environ_sizes_get() -> Result<(size, size), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] environ_sizes_get(Value[] args);

    /// <summary>
    /// clock_res_get(id: clockid) -> Result<timestamp, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] clock_res_get(Value[] args);

    /// <summary>
    /// clock_time_get(id: clockid, precision: timestamp) -> Result<timestamp, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] clock_time_get(Value[] args);

    /// <summary>
    /// fd_advise(fd: fd, offset: filesize, len: filesize, advice: advice) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_advise(Value[] args);

    /// <summary>
    /// fd_allocate(fd: fd, offset: filesize, len: filesize) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_allocate(Value[] args);

    /// <summary>
    /// fd_close(fd: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_close(Value[] args);

    /// <summary>
    /// fd_datasync(fd: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_datasync(Value[] args);

    /// <summary>
    /// fd_fdstat_get(fd: fd) -> Result<fdstat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_fdstat_get(Value[] args);

    /// <summary>
    /// fd_fdstat_set_flags(fd: fd, flags: fdflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_fdstat_set_flags(Value[] args);

    /// <summary>
    /// fd_fdstat_set_rights(fd: fd, fs_rights_base: rights, fs_rights_inheriting: rights) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_fdstat_set_rights(Value[] args);

    /// <summary>
    /// fd_filestat_get(fd: fd) -> Result<filestat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_filestat_get(Value[] args);

    /// <summary>
    /// fd_filestat_set_size(fd: fd, size: filesize) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_filestat_set_size(Value[] args);

    /// <summary>
    /// fd_filestat_set_times(fd: fd, atim: timestamp, mtim: timestamp, fst_flags: fstflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_filestat_set_times(Value[] args);

    /// <summary>
    /// fd_pread(fd: fd, iovs: iovec_array, offset: filesize) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_pread(Value[] args);

    /// <summary>
    /// fd_prestat_get(fd: fd) -> Result<prestat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_prestat_get(Value[] args);

    /// <summary>
    /// fd_prestat_dir_name(fd: fd, path: Pointer<u8>, path_len: size) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_prestat_dir_name(Value[] args);

    /// <summary>
    /// fd_pwrite(fd: fd, iovs: ciovec_array, offset: filesize) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_pwrite(Value[] args);

    /// <summary>
    /// fd_read(fd: fd, iovs: iovec_array) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_read(Value[] args);

    /// <summary>
    /// fd_readdir(fd: fd, buf: Pointer<u8>, buf_len: size, cookie: dircookie) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_readdir(Value[] args);

    /// <summary>
    /// fd_renumber(fd: fd, to: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_renumber(Value[] args);

    /// <summary>
    /// fd_seek(fd: fd, offset: filedelta, whence: whence) -> Result<filesize, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_seek(Value[] args);

    /// <summary>
    /// fd_sync(fd: fd) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_sync(Value[] args);

    /// <summary>
    /// fd_tell(fd: fd) -> Result<filesize, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_tell(Value[] args);

    /// <summary>
    /// fd_write(fd: fd, iovs: ciovec_array) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] fd_write(Value[] args);

    /// <summary>
    /// path_create_directory(fd: fd, path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_create_directory(Value[] args);

    /// <summary>
    /// path_filestat_get(fd: fd, flags: lookupflags, path: string) -> Result<filestat, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_filestat_get(Value[] args);

    /// <summary>
    /// path_filestat_set_times(fd: fd, flags: lookupflags, path: string, atim: timestamp, mtim: timestamp, fst_flags: fstflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_filestat_set_times(Value[] args);

    /// <summary>
    /// path_link(old_fd: fd, old_flags: lookupflags, old_path: string, new_fd: fd, new_path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_link(Value[] args);

    /// <summary>
    /// path_open(fd: fd, dirflags: lookupflags, path: string, oflags: oflags, fs_rights_base: rights, fs_rights_inheriting: rights, fdflags: fdflags) -> Result<fd, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_open(Value[] args);

    /// <summary>
    /// path_readlink(fd: fd, path: string, buf: Pointer<u8>, buf_len: size) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_readlink(Value[] args);

    /// <summary>
    /// path_remove_directory(fd: fd, path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_remove_directory(Value[] args);

    /// <summary>
    /// path_rename(fd: fd, old_path: string, new_fd: fd, new_path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_rename(Value[] args);

    /// <summary>
    /// path_symlink(old_path: string, fd: fd, new_path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_symlink(Value[] args);

    /// <summary>
    /// path_unlink_file(fd: fd, path: string) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] path_unlink_file(Value[] args);

    /// <summary>
    /// poll_oneoff(in: ConstPointer<subscription>, out: Pointer<event>, nsubscriptions: size) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] poll_oneoff(Value[] args);

    /// <summary>
    /// proc_exit(rval: exitcode)
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] proc_exit(Value[] args);

    /// <summary>
    /// proc_raise(sig: signal) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] proc_raise(Value[] args);

    /// <summary>
    /// sched_yield() -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] sched_yield(Value[] args);

    /// <summary>
    /// random_get(buf: Pointer<u8>, buf_len: size) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] random_get(Value[] args);

    /// <summary>
    /// sock_accept(fd: fd, flags: fdflags) -> Result<fd, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] sock_accept(Value[] args);

    /// <summary>
    /// sock_recv(fd: fd, ri_data: iovec_array, ri_flags: riflags) -> Result<(size, roflags), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] sock_recv(Value[] args);

    /// <summary>
    /// sock_send(fd: fd, si_data: ciovec_array, si_flags: siflags) -> Result<size, errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] sock_send(Value[] args);

    /// <summary>
    /// sock_shutdown(fd: fd, how: sdflags) -> Result<(), errno>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Value[] sock_shutdown(Value[] args);

}
